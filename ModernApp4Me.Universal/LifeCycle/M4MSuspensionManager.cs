﻿// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ModernApp4Me.Universal.LifeCycle
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.04.02</since>
  /// <summary>
  /// SuspensionManager captures global session state to simplify process lifetime management
  /// for an application.  Note that session state will be automatically cleared under a variety
  /// of conditions and should only be used to store information that would be convenient to
  /// carry across sessions, but that should be discarded when an application crashes or is upgraded.
  /// </summary>
  // Inspired by the Suspension Manager class of the HubPage template
  public abstract class M4MSuspensionManager<SuspensionManagerClass> 
    where SuspensionManagerClass : M4MSuspensionManager<SuspensionManagerClass>, new()
{

    protected const string SESSION_STATE_FILENAME = "_sessionState.xml";

    protected readonly List<Type> knownTypes = new List<Type>();

    protected Dictionary<string, object> sessionState = new Dictionary<string, object>();

    /// <summary>
    /// Provides access to global session state for the current session.  This state is
    /// serialized by <see cref="SaveAsync"/> and restored by
    /// <see cref="RestoreAsync"/>, so values must be serializable by
    /// <see cref="DataContractSerializer"/> and should be as compact as possible.  Strings
    /// and other self-contained data types are strongly recommended.
    /// </summary>
    public Dictionary<string, object> SessionState
    {
       get { return sessionState; }
    }

    /// <summary>
    /// List of custom types provided to the <see cref="DataContractSerializer"/> when
    /// reading and writing session state.  Initially empty, additional types may be
    /// added to customize the serialization process.
    /// </summary>
    public List<Type> KnownTypes
    {
      get { return knownTypes; }
    }

    protected static volatile SuspensionManagerClass instance;

    protected static readonly object InstanceLock = new Object();

    public static SuspensionManagerClass Instance
    {
      get
      {
        if (instance == null)
        {
          lock (InstanceLock)
          {
            if (instance == null)
            {
              instance = new SuspensionManagerClass();
            }
          }
        }

        return instance;
      }
    }

    /// <summary>
    /// Save the current <see cref="SessionState"/>.  Any <see cref="Frame"/> instances
    /// registered with <see cref="RegisterFrame"/> will also preserve their current
    /// navigation stack, which in turn gives their active <see cref="Page"/> an opportunity
    /// to save its state.
    /// </summary>
    /// <returns>An asynchronous task that reflects when session state has been saved.</returns>
    public virtual async Task SaveAsync()
    {
      try
      {
        // Save the navigation state for all registered frames
        foreach (var weakFrameReference in RegisteredFrames)
        {
          Frame frame;
          if (weakFrameReference.TryGetTarget(out frame))
          {
            SaveFrameNavigationState(frame);
          }
        }

        // Serialize the session state synchronously to avoid asynchronous access to shared
        // state
        var sessionData = new MemoryStream();
        var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), knownTypes);
        serializer.WriteObject(sessionData, sessionState);

        // Get an output stream for the SessionState file and write the state asynchronously
        var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(M4MSuspensionManager<SuspensionManagerClass>.SESSION_STATE_FILENAME, CreationCollisionOption.ReplaceExisting);
        using (var fileStream = await file.OpenStreamForWriteAsync())
        {
          sessionData.Seek(0, SeekOrigin.Begin);
          await sessionData.CopyToAsync(fileStream);
        }
      }
      catch (Exception exception)
      {
        throw new M4MSuspensionManagerException(M4MSuspensionManagerException.SuspensionManagerOperation.Save, exception);
      }
    }

    /// <summary>
    /// Restores previously saved <see cref="SessionState"/>.  Any <see cref="Frame"/> instances
    /// registered with <see cref="RegisterFrame"/> will also restore their prior navigation
    /// state, which in turn gives their active <see cref="Page"/> an opportunity restore its
    /// state.
    /// </summary>
    /// <param name="sessionBaseKey">An optional key that identifies the type of session.
    /// This can be used to distinguish between multiple application launch scenarios.</param>
    /// <returns>An asynchronous task that reflects when session state has been read.  The
    /// content of <see cref="SessionState"/> should not be relied upon until this task
    /// completes.</returns>
    public virtual async Task RestoreAsync(String sessionBaseKey = null)
    {
      sessionState = new Dictionary<String, Object>();

      try
      {
        // Get the input stream for the SessionState file
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync(M4MSuspensionManager<SuspensionManagerClass>.SESSION_STATE_FILENAME);
        using (var inStream = await file.OpenSequentialReadAsync())
        {
          // Deserialize the Session State
          var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), knownTypes);
          sessionState = (Dictionary<string, object>)serializer.ReadObject(inStream.AsStreamForRead());
        }

        // Restore any registered frames to their saved state
        foreach (var weakFrameReference in RegisteredFrames)
        {
          Frame frame;
          if (weakFrameReference.TryGetTarget(out frame) && (string)frame.GetValue(FrameSessionBaseKeyProperty) == sessionBaseKey)
          {
            frame.ClearValue(FrameSessionStateProperty);
            RestoreFrameNavigationState(frame);
          }
        }
      }
      catch (Exception exception)
      {
        throw new M4MSuspensionManagerException(M4MSuspensionManagerException.SuspensionManagerOperation.Restore, exception);
      }
    }

    protected static readonly List<WeakReference<Frame>> RegisteredFrames = new List<WeakReference<Frame>>();

    protected static readonly DependencyProperty FrameSessionBaseKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionBaseKeyParams", typeof(String), typeof(SuspensionManagerClass), null);

    protected static readonly DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof(String), typeof(SuspensionManagerClass), null);

    protected static readonly DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached("_FrameSessionState", typeof(Dictionary<String, Object>), typeof(SuspensionManagerClass), null);

    /// <summary>
    /// Registers a <see cref="Frame"/> instance to allow its navigation history to be saved to
    /// and restored from <see cref="SessionState"/>.  Frames should be registered once
    /// immediately after creation if they will participate in session state management.  Upon
    /// registration if state has already been restored for the specified key
    /// the navigation history will immediately be restored.  Subsequent invocations of
    /// <see cref="RestoreAsync"/> will also restore navigation history.
    /// </summary>
    /// <param name="frame">An instance whose navigation history should be managed by
    /// <see cref="M4MSuspensionManager{SuspensionManagerClass}"/></param>
    /// <param name="sessionStateKey">A unique key into <see cref="SessionState"/> used to
    /// store navigation-related information.</param>
    /// <param name="sessionBaseKey">An optional key that identifies the type of session.
    /// This can be used to distinguish between multiple application launch scenarios.</param>
    public virtual void RegisterFrame(Frame frame, String sessionStateKey, String sessionBaseKey = null)
    {
      if (frame.GetValue(FrameSessionStateKeyProperty) != null)
      {
        throw new InvalidOperationException("Frames can only be registered to one session state key");
      }

      if (frame.GetValue(FrameSessionStateProperty) != null)
      {
        throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
      }

      if (!string.IsNullOrEmpty(sessionBaseKey))
      {
        frame.SetValue(FrameSessionBaseKeyProperty, sessionBaseKey);
        sessionStateKey = sessionBaseKey + "_" + sessionStateKey;
      }

      // Use a dependency property to associate the session key with a frame, and keep a list of frames whose
      // navigation state should be managed
      frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey);
      RegisteredFrames.Add(new WeakReference<Frame>(frame));

      // Check to see if navigation state can be restored
        RestoreFrameNavigationState(frame);
    }

    /// <summary>
    /// Disassociates a <see cref="Frame"/> previously registered by <see cref="RegisterFrame"/>
    /// from <see cref="SessionState"/>.  Any navigation state previously captured will be
    /// removed.
    /// </summary>
    /// <param name="frame">An instance whose navigation history should no longer be
    /// managed.</param>
    public virtual void UnregisterFrame(Frame frame)
    {
      // Remove session state and remove the frame from the list of frames whose navigation
      // state will be saved (along with any weak references that are no longer reachable)
      SessionState.Remove((String)frame.GetValue(FrameSessionStateKeyProperty));
      RegisteredFrames.RemoveAll((weakFrameReference) =>
      {
        Frame testFrame;
        return !weakFrameReference.TryGetTarget(out testFrame) || testFrame == frame;
      });
    }

    /// <summary>
    /// Provides storage for session state associated with the specified <see cref="Frame"/>.
    /// Frames that have been previously registered with <see cref="RegisterFrame"/> have
    /// their session state saved and restored automatically as a part of the global
    /// <see cref="SessionState"/>.  Frames that are not registered have transient state
    /// that can still be useful when restoring pages that have been discarded from the
    /// navigation cache.
    /// </summary>
    /// <remarks>Apps may choose to rely on <see cref="M4MNavigationHelper"/> to manage
    /// page-specific state instead of working with frame session state directly.</remarks>
    /// <param name="frame">The instance for which session state is desired.</param>
    /// <returns>A collection of state subject to the same serialization mechanism as
    /// <see cref="SessionState"/>.</returns>
    public virtual Dictionary<String, Object> SessionStateForFrame(Frame frame)
    {
      var frameState = (Dictionary<String, Object>)frame.GetValue(FrameSessionStateProperty);

      if (frameState == null)
      {
        var frameSessionKey = (String)frame.GetValue(FrameSessionStateKeyProperty);
        if (frameSessionKey != null)
        {
          // Registered frames reflect the corresponding session state
          if (!sessionState.ContainsKey(frameSessionKey))
          {
              sessionState[frameSessionKey] = new Dictionary<String, Object>();
          }

          frameState = (Dictionary<String, Object>)sessionState[frameSessionKey];
        }
        else
        {
          // Frames that aren't registered have transient state
          frameState = new Dictionary<String, Object>();
        }

        frame.SetValue(FrameSessionStateProperty, frameState);
      }

      return frameState;
    }

    protected virtual void RestoreFrameNavigationState(Frame frame)
    {
      var frameState = SessionStateForFrame(frame);

      if (frameState.ContainsKey("Navigation"))
      {
        frame.SetNavigationState((String)frameState["Navigation"]);
      }
    }

    protected virtual void SaveFrameNavigationState(Frame frame)
    {
      var frameState = SessionStateForFrame(frame);
      frameState["Navigation"] = frame.GetNavigationState();
    }

  }

}
