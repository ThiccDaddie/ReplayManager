namespace ReplayManager.Shared
{
	using System;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;
	public class NotifyTaskCompletion : INotifyPropertyChanged
	{
		public NotifyTaskCompletion(Task task)
		{
			Task = task;
			if (!task.IsCompleted)
			{
				var _ = WatchTaskAsync(task);
			}
		}
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
		public virtual Task Task { get; private set; }
		public TaskStatus Status { get { return Task.Status; } }
		public bool IsCompleted { get { return Task.IsCompleted; } }
		public bool IsNotCompleted { get { return !Task.IsCompleted; } }
		public bool IsSuccessfullyCompleted
		{
			get
			{
				return Task.Status ==
	TaskStatus.RanToCompletion;
			}
		}
		public bool IsCanceled { get { return Task.IsCanceled; } }
		public bool IsFaulted { get { return Task.IsFaulted; } }
		public AggregateException Exception { get { return Task.Exception; } }
		public Exception InnerException
		{
			get
			{
				return Exception?.InnerException;
			}
		}
		public string ErrorMessage
		{
			get
			{
				return InnerException?.Message;
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		// Use a wrapping method so we can raise PropertyChanged from a deriving class
		protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class NotifyTaskCompletion<TResult> : NotifyTaskCompletion
	{
		public NotifyTaskCompletion(Task<TResult> task) : base(task)
		{
		}

		public TResult Result
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