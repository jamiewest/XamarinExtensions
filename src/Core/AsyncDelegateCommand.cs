﻿//
// AsyncDelegateCommand.cs
//
// Author:
//       Mark Smith <smmark@microsoft.com>
//
// Copyright (c) 2016-2018 Xamarin, Microsoft.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Threading.Tasks;

namespace West.Extensions.Xamarin
{
    /// <summary>
    /// A base ICommand implementation that supports async/await.
    /// </summary>
	public class AsyncDelegateCommand : IAsyncDelegateCommand
    {
        /// <summary>
        /// Delegate to call when CanExecute method is called.
        /// </summary>
		protected readonly Predicate<object> canExecute;

        /// <summary>
        /// Delegate to call when Execute is called.
        /// </summary>
		protected Func<object, Task> asyncExecute;

        /// <summary>
        /// Event which is raised when the state of this command has changed.
        /// </summary>
		public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a new async delegate command.
        /// </summary>
        /// <param name="execute">Method to call when command is executed.</param>
		public AsyncDelegateCommand(Func<Task> execute)
            : this(_ => execute(), null)
        {
        }

        /// <summary>
        /// Creates a new async delegate command.
        /// </summary>
        /// <param name="execute">Method to call when command is executed.</param>
		public AsyncDelegateCommand(Func<object, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new async delegate command.
        /// </summary>
        /// <param name="execute">Method to call when command is executed.</param>
        /// <param name="canExecute">Method to call to determine whether command is valid.</param>
        public AsyncDelegateCommand(Func<Task> execute, Func<bool> canExecute)
            : this(_ => execute(), _ => canExecute())
        {
        }

        /// <summary>
        /// Creates a new async delegate command.
        /// </summary>
        /// <param name="asyncExecute">Method to call when command is executed.</param>
        /// <param name="canExecute">Method to call to determine whether command is valid.</param>
		public AsyncDelegateCommand(Func<object, Task> asyncExecute,
            Predicate<object> canExecute)
        {
            this.asyncExecute = asyncExecute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Raise the CanExecuteChanged handler.
        /// </summary>
		public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns whether the command is possible right now.
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
		public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
		public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /// <summary>
        /// Executes the command and returns an awaitable task.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameter">Parameter.</param>
		public async Task ExecuteAsync(object parameter)
        {
            await asyncExecute(parameter);
        }
    }

    /// <summary>
    /// A generic ICommand implementation that supports async/await.
    /// </summary>
    public class AsyncDelegateCommand<T> : IAsyncDelegateCommand<T>
    {
        /// <summary>
        /// Delegate to call when CanExecute method is called.
        /// </summary>
		protected readonly Predicate<T> canExecute;

        /// <summary>
        /// Delegate to call when Execute method is called.
        /// </summary>
        protected Func<T, Task> asyncExecute;

        /// <summary>
        /// Event to raise when the state of the command has changed.
        /// </summary>
		public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a new async delegate command.
        /// </summary>
        /// <param name="execute">Method to call when command is executed.</param>
        public AsyncDelegateCommand(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new async delegate command.
        /// </summary>
        /// <param name="asyncExecute">Method to call when command is executed.</param>
        /// <param name="canExecute">Method to determine whether command is valid.</param>
        public AsyncDelegateCommand(Func<T, Task> asyncExecute,
            Predicate<T> canExecute)
        {
            this.asyncExecute = asyncExecute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
		public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns whether the command is valid at this moment.
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
		public bool CanExecute(object parameter)
        {
            return (canExecute == null) || canExecute((T)parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
		public async void Execute(object parameter)
        {
            await ExecuteAsync((T)parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        async Task IAsyncDelegateCommand.ExecuteAsync(object parameter)
        {
            await asyncExecute((T)parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
		public async Task ExecuteAsync(T parameter)
        {
            await asyncExecute(parameter);
        }
    }
}