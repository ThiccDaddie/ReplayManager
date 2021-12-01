// <copyright file="NotifyTaskCompletion.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ReplayManager.Models
{
	public class NotifyTaskCompletion : INotifyPropertyChanged
	{
		public NotifyTaskCompletion(Task task)
		{
			this.Task = task;
			if (!task.IsCompleted)
			{
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
				var _ = WatchTaskAsync(task);
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public TaskStatus Status
		{
			get { return Task.Status; }
		}

		public bool IsCompleted
		{
			get { return Task.IsCompleted; }
		}

		public bool IsNotCompleted
		{
			get { return !Task.IsCompleted; }
		}

		public bool IsSuccessfullyCompleted
		{
			get
			{
				return Task.Status ==
	TaskStatus.RanToCompletion;
			}
		}

		public bool IsCanceled
		{
			get { return Task.IsCanceled; }
		}

		public bool IsFaulted
		{
			get { return Task.IsFaulted; }
		}

		public AggregateException? Exception
		{
			get { return Task.Exception; }
		}

		public Exception? InnerException
		{
			get
			{
				return Exception?.InnerException;
			}
		}

		public string? ErrorMessage
		{
			get
			{
				return InnerException?.Message;
			}
		}

		protected Task Task { get; set; }

		protected virtual async Task WatchTaskAsync(Task task)
		{
			try
			{
				await task;
			}
			catch
			{
			}

			NotifyPropertyChanged(nameof(Status));
			NotifyPropertyChanged(nameof(IsCompleted));
			NotifyPropertyChanged(nameof(IsNotCompleted));
			if (task.IsCanceled)
			{
				NotifyPropertyChanged(nameof(IsCanceled));
			}
			else if (task.IsFaulted)
			{
				NotifyPropertyChanged(nameof(IsFaulted));
				NotifyPropertyChanged(nameof(Exception));
				NotifyPropertyChanged(nameof(InnerException));
				NotifyPropertyChanged(nameof(ErrorMessage));
			}
			else
			{
				NotifyPropertyChanged(nameof(IsSuccessfullyCompleted));
				NotifyPropertyChanged("Result");
			}
		}

		// Use a wrapping method so we can raise PropertyChanged from a deriving class
		protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}