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
