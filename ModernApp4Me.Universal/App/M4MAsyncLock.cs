// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModernApp4Me.Universal.App
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.04.03</since>
  // Taken from http://www.hanselman.com/blog/ComparingTwoTechniquesInNETAsynchronousCoordinationPrimitives.aspx
  // Taken from http://blogs.msdn.com/b/pfxteam/archive/2012/02/12/10266988.aspx
  public sealed class M4MAsyncLock
  {

    private sealed class M4MReleaser : IDisposable
    {

      private readonly M4MAsyncLock toRelease;

      internal M4MReleaser(M4MAsyncLock toRelease)
      {
        this.toRelease = toRelease;
      }

      public void Dispose()
      {
        toRelease.semaphore.Release();
      }
    }

    private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    private readonly Task<IDisposable> releaser;

    public M4MAsyncLock()
    {
        releaser = Task.FromResult((IDisposable)new M4MReleaser(this));
    }
 
    public Task<IDisposable> LockAsync()
    {
      var wait = semaphore.WaitAsync();

      return wait.IsCompleted
        ? releaser
        : wait.ContinueWith((_, state) => (IDisposable) state, releaser.Result, CancellationToken.None,
          TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

  }

}
