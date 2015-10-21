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
using System.Windows.Input;

namespace ModernApp4Me.Universal.LifeCycle
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.04.02</since>
  /// <summary>
  /// A command whose sole purpose is to relay its functionality 
  /// to other objects by invoking delegates. 
  /// The default return value for the CanExecute method is 'true'.
  /// <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
  /// <see cref="CanExecute"/> is expected to return a different value.
  /// </summary>
  // Taken from the HubPage template
  public class M4MRelayCommand : ICommand
  {

    private readonly Action execute;

    private readonly Func<bool> canExecute;

    /// <summary>
    /// Raised when RaiseCanExecuteChanged is called.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Creates a new command that can always execute.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public M4MRelayCommand(Action execute) : this(execute, null)
    {
    }

    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public M4MRelayCommand(Action execute, Func<bool> canExecute)
    {
      if (execute == null)
      {
        throw new ArgumentNullException("execute");
      }

      this.execute = execute;
      this.canExecute = canExecute;
    }

    /// <summary>
    /// Determines whether this <see cref="M4MRelayCommand"/> can execute in its current state.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
    /// </param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter)
    {
      return canExecute == null ? true : canExecute();
    }

    /// <summary>
    /// Executes the <see cref="M4MRelayCommand"/> on the current command target.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
    /// </param>
    public void Execute(object parameter)
    {
        execute();
    }

    /// <summary>
    /// Method used to raise the <see cref="CanExecuteChanged"/> event
    /// to indicate that the return value of the <see cref="CanExecute"/>
    /// method has changed.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
      var handler = CanExecuteChanged;

      if (handler != null)
      {
        handler(this, EventArgs.Empty);
      }
    }

  }

}