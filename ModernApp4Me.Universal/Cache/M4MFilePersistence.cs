// The MIT License (MIT)
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using ModernApp4Me.Universal.App;

namespace ModernApp4Me.Universal.Cache
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2014.03.24</since>
  /// <summary>
  /// Enables to store some contents into a specific <see cref="StorageFolder"/>.
  /// The classe implements the singleton pattern and is thread safe !
  /// </summary>
  public sealed class M4MFilePersistence
  {

    private static volatile M4MFilePersistence instance;

    private static readonly M4MAsyncLock InstanceLock = new M4MAsyncLock();

    private M4MFilePersistence()
    {
    }

    public static M4MFilePersistence Instance
    {
      get
      {
        if (instance == null)
        {
          lock (InstanceLock)
          {
            if (instance == null)
            {
              instance = new M4MFilePersistence();
            }
          }
        }

        return instance;
      }
    }

    /// <summary>
    /// Checks if a file exists into the <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="fileName">the name of the file</param>
    /// <returns>true if the file exists, false otherwise</returns>
    public async Task<bool> IsFileExists(StorageFolder storageFolder, string fileName)
    {
      using (await InstanceLock.LockAsync())
      {
        try
        {
          fileName = fileName.Replace("/", "\\");
          var file = await storageFolder.GetFileAsync(fileName);
          return (file != null);
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Reads a file from a specified <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="fileName">the name of the file to read</param>
    /// <returns>the content of the file in case of success, null otherwise</returns>
    public async Task<string> ReadFile(StorageFolder storageFolder, string fileName)
    {
      using (await InstanceLock.LockAsync())
      {
        string fileContent = null;

        try
        {
          fileName = fileName.Replace("/", "\\");
          var file = await storageFolder.GetFileAsync(fileName);
          if (file != null)
          {
            fileContent = await FileIO.ReadTextAsync(file);
          }
        }
        catch
        {
          fileContent = null;
        }

        return fileContent;
      }
    }

    /// <summary>
    /// Writes a <see cref="string"/> into a file located into a specified <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="fileName">the file name</param>
    /// <param name="fileContent">the file content</param>
    /// <param name="creationOption">the <see cref="CreationCollisionOption"/></param>
    /// <returns>true in case of success. false otherwise</returns>
    public async Task<bool> WriteFile(StorageFolder storageFolder, string fileName, string fileContent, CreationCollisionOption creationOption)
    {
      using (await InstanceLock.LockAsync())
      {
        var isWritten = true;

        try
        {
          fileName = fileName.Replace("/", "\\");
          var file = await storageFolder.CreateFileAsync(fileName, creationOption);
          await FileIO.WriteTextAsync(file, fileContent);
        }
        catch
        {
          isWritten = false;
        }

        return isWritten;
      }
    }

    /// <summary>
    /// Delete a file from a specified <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="fileName">the name of the file to delete</param>
    /// <returns>true  in case of success, null otherwise</returns>
    public async Task<bool> DeleteFile(StorageFolder storageFolder, string fileName)
    {
      using (await InstanceLock.LockAsync())
      {
        var isDeleted = true;

        try
        {
          fileName = fileName.Replace("/", "\\");
          var file = await storageFolder.GetFileAsync(fileName);
          if (file != null)
          {
            await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
          }
        }
        catch
        {
          isDeleted = false;
        }

        return isDeleted;
      }
    }

    /// <summary>
    /// Copy a file from a source <see cref="StorageFolder"/> to a destination <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="source">the source folder containing the file</param>
    /// <param name="destination">the destination folder where write the new file</param>
    /// <param name="fileName">the file name</param>
    /// <param name="destinationFileName">the destination file name (if different from the source)</param>
    /// <returns></returns>
    public async Task<bool> CopyFile(StorageFolder source, StorageFolder destination, string fileName, string destinationFileName = null)
    {
      //using (await InstanceLock.LockAsync())
      //{
        var isCopy = true;

        try
        {
          fileName = fileName.Replace("/", "\\");
          destinationFileName = (String.IsNullOrEmpty(destinationFileName) == true) ? fileName : destinationFileName.Replace("/", "\\");
          var fileContent = await ReadFile(source, fileName);
          await WriteFile(destination, destinationFileName, fileContent, CreationCollisionOption.ReplaceExisting);
        }
        catch
        {
          isCopy = false;
        }

        return isCopy;
      //}
    }

    /// <summary>
    /// Move a file from a source <see cref="StorageFolder"/> to a destination <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="source">the source folder containing the file</param>
    /// <param name="destination">the destination folder where write the new file</param>
    /// <param name="fileName">the file name</param>
    /// <param name="destinationFileName">the destination file name (if different from the source)</param>
    /// <returns></returns>
    public async Task<bool> MoveFile(StorageFolder source, StorageFolder destination, string fileName, string destinationFileName = null)
    {
      //using (await InstanceLock.LockAsync())
      //{
      var success = true;

      try
      {
        success = await CopyFile(source, destination, fileName, destinationFileName);
        if (success == true)
        {
          success = await DeleteFile(source, fileName);
        }
      }
      catch
      {
        success = false;
      }

      return success;
      //}
    }

    /// <summary>
    /// Checks if a folder exists into the <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="folderName">the name of the folder</param>
    /// <returns>true if the folder exists, false otherwise</returns>
    public async Task<bool> IsFolderExists(StorageFolder storageFolder, string folderName)
    {
      using (await InstanceLock.LockAsync())
      {
        try
        {
          folderName = folderName.Replace("/", "\\");
          var file = await storageFolder.GetFolderAsync(folderName);
          return (file != null);
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Get the free space of a <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <returns>free space or null in case of errors</returns>
    public async Task<UInt64?> GetFreeSpace(StorageFolder storageFolder)
    {
      using (await InstanceLock.LockAsync())
      {
        try
        {
          var properties = await storageFolder.Properties.RetrievePropertiesAsync(new [] { "System.FreeSpace" });
          return Convert.ToUInt64(properties["System.FreeSpace"]);
        }
        catch
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Get the used space of a <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <returns>free space or null in case of errors</returns>
    public async Task<UInt64?> GetUsedSpace(StorageFolder storageFolder)
    {
      using (await InstanceLock.LockAsync())
      {
        try
        {
          var properties = await storageFolder.Properties.RetrievePropertiesAsync(new [] { "System.FreeSpace", "System.Capacity" });
          var freeSpace = Convert.ToUInt64(properties["System.FreeSpace"]);
          var capacity = Convert.ToUInt64(properties["System.Capacity"]);
          return capacity - freeSpace;
        }
        catch
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Creates a <see cref="StorageFolder"/> into a specified <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="folderName">the folder name</param>
    /// <param name="creationOption">the <see cref="CreationCollisionOption"/></param>
    /// <returns>true in case of success. false otherwise</returns>
    public async Task<bool> CreateFolder(StorageFolder storageFolder, string folderName, CreationCollisionOption creationOption)
    {
      using (await InstanceLock.LockAsync())
      {
        var isCreated = true;

        try
        {
          folderName = folderName.Replace("/", "\\");
          var folder = await storageFolder.CreateFolderAsync(Path.GetDirectoryName(folderName), creationOption);
        }
        catch
        {
          isCreated = false;
        }

        return isCreated;
      }
    }

    /// <summary>
    /// Creates a directory into a specified <see cref="StorageFolder"/> by creating all missing <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="path">the directory path</param>
    /// <returns>true in case of success. false otherwise</returns>
    public async Task<bool> CreateDirectory(StorageFolder storageFolder, string path)
    {
      using (await InstanceLock.LockAsync())
      {
        var isCreated = true;

        try
        {
          var folderNames = path.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();

          // Cleans ".." in uri
          while (folderNames.Any(f => f.Equals("..")) == true)
          {
            var index = folderNames.FindIndex(f => f.Equals(".."));
            if (index == 0)
            {
              throw new ArgumentException("Invalid specified path, can't access of the parent of the source folder.");
            }
            folderNames.RemoveRange(index - 1, 2);
          }

          // Creates all missing folders
          foreach(var folderName in folderNames)
          {
            storageFolder = await storageFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
          }
        }
        catch
        {
          isCreated = false;
        }

        return isCreated;
      }
    }

    /// <summary>
    /// Removes a <see cref="StorageFolder"/> into a specified <see cref="StorageFolder"/>
    /// </summary>
    /// <param name="storageFolder">the folder where the function has to check</param>
    /// <param name="folderName">the folder name</param>
    /// <returns>true in case of success. false otherwise</returns>
    public async Task<bool> DeleteFolder(StorageFolder storageFolder, string folderName = null)
    {
      using (await InstanceLock.LockAsync())
      {
        var isDeleted = true;

        try
        {
          var folder = (String.IsNullOrEmpty(folderName) == true) ? storageFolder : await storageFolder.GetFolderAsync(folderName.Replace("/", "\\"));
          if (folder != null)
          {
            await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
          }
        }
        catch
        {
          isDeleted = false;
        }

        return isDeleted;
      }
    }

    /// <summary>
    /// Copy a <see cref="StorageFolder"/> and its content to a specified directory
    /// </summary>
    /// <param name="source">the source folder</param>
    /// <param name="destination">the destination folder where create the copy</param>
    /// <param name="directoryName">the copy directory</param>
    /// <returns></returns>
    public async Task<bool> CopyFolder(StorageFolder source, StorageFolder destination, string directoryName)
    {
      //using (await InstanceLock.LockAsync())
      //{
      var isCopied = true;

      try
      {
        directoryName = directoryName.Replace("/", "\\");
        await CreateDirectory(destination, directoryName);
        var folderDest = await destination.GetFolderAsync(directoryName);
        if (folderDest != null)
        {
          var items = await source.GetItemsAsync();
          foreach (var item in items)
          {
            if (item.IsOfType(StorageItemTypes.File) == true)
            {
              isCopied = await CopyFile(source, folderDest, item.Name);
            }
            else if (item.IsOfType(StorageItemTypes.Folder) == true)
            {
              isCopied = await CopyFolder(source, folderDest, item.Name);
            }

            if (isCopied == false)
            {
              throw new Exception("Fail to copy item");
            }
          }
        }
        else
        {
          throw new Exception("Fail to create new directory");
        }
      }
      catch
      {
        isCopied = false;
      }

      return isCopied;
      //}
    }

    /// <summary>
    /// Move a <see cref="StorageFolder"/> and its content to a specified directory
    /// </summary>
    /// <param name="source">the source folder</param>
    /// <param name="destination">the destination folder where create the copy</param>
    /// <param name="directoryName">the copy directory</param>
    /// <returns></returns>
    public async Task<bool> MoveFolder(StorageFolder source, StorageFolder destination, string directoryName)
    {
      //using (await InstanceLock.LockAsync())
      //{
      var success = true;

      try
      {
        success = await CopyFolder(source, destination, directoryName);
        if (success == true)
        {
          success = await DeleteFolder(source);
        }
      }
      catch
      {
        success = false;
      }

      return success;
      //}
    }

  }

}
