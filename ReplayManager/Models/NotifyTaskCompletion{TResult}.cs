// <copyright file="NotifyTaskCompletion{TResult}.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

namespace ReplayManager.Models
{
	public class NotifyTaskCompletion<TResult> : NotifyTaskCompletion
	{
		public NotifyTaskCompletion(Task<TResult> task)
			: base(task)
		{
		}

		public TResult? Result
		{
			get
			{
				if (Task is Task<TResult> task)
				{
					return (task.Status == TaskStatus.RanToCompletion)
						? task.Result
						: default;
				}

				return default;
			}
		}

		protected override async Task WatchTaskAsync(Task task)
		{
			await base.WatchTaskAsync(task);
			if (!task.IsCanceled && !task.IsFaulted)
			{
				NotifyPropertyChanged(nameof(Result));
			}
		}
	}
}