// <copyright file="DebounceDispatcher.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReplayManager.Helpers
{
	/// <summary>
	/// Provides Debounce() and Throttle() methods.
	/// Use these methods to ensure that events aren't handled too frequently.
	///
	/// Throttle() ensures that events are throttled by the interval specified.
	/// Only the last event in the interval sequence of events fires.
	///
	/// Debounce() fires an event only after the specified interval has passed
	/// in which no other pending event has fired. Only the last event in the
	/// sequence is fired.
	/// </summary>
	public class DebounceDispatcher
	{
		private Timer timer;

		private DateTime TimerStarted { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Debounce an event by resetting the event timeout every time the event is
		/// fired. The behavior is that the Action passed is fired only after events
		/// stop firing for the given timeout period.
		///
		/// Use Debounce when you want events to fire only after events stop firing
		/// after the given interval timeout period.
		///
		/// Wrap the logic you would normally use in your event code into
		/// the  Action you pass to this method to debounce the event.
		/// Example: https://gist.github.com/RickStrahl/0519b678f3294e27891f4d4f0608519a.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		public async Task Debounce(int interval, Action action)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			// timer is recreated for each event and effectively
			// resets the timeout. Action only fires after timeout has fully
			// elapsed without other events firing in between
			timer = new Timer(
				(_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				action.Invoke();
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// Debounce an event by resetting the event timeout every time the event is
		/// fired. The behavior is that the Action passed is fired only after events
		/// stop firing for the given timeout period.
		///
		/// Use Debounce when you want events to fire only after events stop firing
		/// after the given interval timeout period.
		///
		/// Wrap the logic you would normally use in your event code into
		/// the  Action you pass to this method to debounce the event.
		/// Example: https://gist.github.com/RickStrahl/0519b678f3294e27891f4d4f0608519a.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		public async Task Debounce(int interval, Func<Task> action)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			// timer is recreated for each event and effectively
			// resets the timeout. Action only fires after timeout has fully
			// elapsed without other events firing in between
			timer = new Timer(
				async (_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				await action.Invoke();
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// Debounce an event by resetting the event timeout every time the event is
		/// fired. The behavior is that the Action passed is fired only after events
		/// stop firing for the given timeout period.
		///
		/// Use Debounce when you want events to fire only after events stop firing
		/// after the given interval timeout period.
		///
		/// Wrap the logic you would normally use in your event code into
		/// the  Action you pass to this method to debounce the event.
		/// Example: https://gist.github.com/RickStrahl/0519b678f3294e27891f4d4f0608519a.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		/// <param name="param">optional parameter.</param>
		public async Task Debounce<T>(
			int interval,
			Action<T> action,
			T param = default)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			// timer is recreated for each event and effectively
			// resets the timeout. Action only fires after timeout has fully
			// elapsed without other events firing in between
			timer = new Timer(
				(_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				action.Invoke(param);
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// Debounce an event by resetting the event timeout every time the event is
		/// fired. The behavior is that the Action passed is fired only after events
		/// stop firing for the given timeout period.
		///
		/// Use Debounce when you want events to fire only after events stop firing
		/// after the given interval timeout period.
		///
		/// Wrap the logic you would normally use in your event code into
		/// the  Action you pass to this method to debounce the event.
		/// Example: https://gist.github.com/RickStrahl/0519b678f3294e27891f4d4f0608519a.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		/// <param name="param">optional parameter.</param>
		public async Task Debounce<T>(
			int interval,
			Func<T, Task> action,
			T param = default)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			// timer is recreated for each event and effectively
			// resets the timeout. Action only fires after timeout has fully
			// elapsed without other events firing in between
			timer = new Timer(
				async (_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				await action.Invoke(param);
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// This method throttles events by allowing only 1 event to fire for the given
		/// timeout period. Only the last event fired is handled - all others are ignored.
		/// Throttle will fire events every timeout ms even if additional events are pending.
		///
		/// Use Throttle where you need to ensure that events fire at given intervals.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		public async Task Throttle(int interval, Action action)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			var curTime = DateTime.UtcNow;

			// if timeout is not up yet - adjust timeout to fire
			// with potentially new Action parameters
			if (curTime.Subtract(TimerStarted).TotalMilliseconds > interval)
			{
				TimerStarted = curTime;
			}
			else
			{
				interval -= (int)curTime.Subtract(TimerStarted).TotalMilliseconds;
			}

			timer = new Timer(
				(_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				action.Invoke();
				TimerStarted = curTime;
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// This method throttles events by allowing only 1 event to fire for the given
		/// timeout period. Only the last event fired is handled - all others are ignored.
		/// Throttle will fire events every timeout ms even if additional events are pending.
		///
		/// Use Throttle where you need to ensure that events fire at given intervals.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		/// <param name="param">optional parameter.</param>
		public async Task Throttle<T>(
			int interval,
			Action<T> action,
			T param = default)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			var curTime = DateTime.UtcNow;

			// if timeout is not up yet - adjust timeout to fire
			// with potentially new Action parameters
			if (curTime.Subtract(TimerStarted).TotalMilliseconds > interval)
			{
				TimerStarted = curTime;
			}
			else
			{
				interval -= (int)curTime.Subtract(TimerStarted).TotalMilliseconds;
			}

			timer = new Timer(
				(_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				action.Invoke(param);
				TimerStarted = curTime;
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// This method throttles events by allowing only 1 event to fire for the given
		/// timeout period. Only the last event fired is handled - all others are ignored.
		/// Throttle will fire events every timeout ms even if additional events are pending.
		///
		/// Use Throttle where you need to ensure that events fire at given intervals.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		public async Task Throttle(int interval, Func<Task> action)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			var curTime = DateTime.UtcNow;

			// if timeout is not up yet - adjust timeout to fire
			// with potentially new Action parameters
			if (curTime.Subtract(TimerStarted).TotalMilliseconds > interval)
			{
				TimerStarted = curTime;
			}
			else
			{
				interval -= (int)curTime.Subtract(TimerStarted).TotalMilliseconds;
			}

			timer = new Timer(
				async (_) =>
				   {
					   if (timer == null)
					   {
						   return;
					   }

					   timer?.Dispose();
					   timer = null;
					   await action.Invoke();
					   TimerStarted = curTime;
				   },
				null,
				interval <= 0 ? TimeSpan.Zero : TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// This method throttles events by allowing only 1 event to fire for the given
		/// timeout period. Only the last event fired is handled - all others are ignored.
		/// Throttle will fire events every timeout ms even if additional events are pending.
		///
		/// Use Throttle where you need to ensure that events fire at given intervals.
		/// </summary>
		/// <param name="interval">Timeout in Milliseconds.</param>
		/// <param name="action">Action.<object> to fire when debounced event fires</object></param>
		/// <param name="param">optional parameter.</param>
		public async Task Throttle<T>(
			int interval,
			Func<T, Task> action,
			T param = default)
		{
			// kill pending timer and pending ticks
			if (timer is not null)
			{
				await timer.DisposeAsync();
			}

			timer = null;

			var curTime = DateTime.UtcNow;

			// if timeout is not up yet - adjust timeout to fire
			// with potentially new Action parameters
			if (curTime.Subtract(TimerStarted).TotalMilliseconds > interval)
			{
				TimerStarted = curTime;
			}
			else
			{
				interval -= (int)curTime.Subtract(TimerStarted).TotalMilliseconds;
			}

			timer = new Timer(
				async (_) =>
			{
				if (timer == null)
				{
					return;
				}

				timer?.Dispose();
				timer = null;
				await action.Invoke(param);
				TimerStarted = curTime;
			},
				null,
				TimeSpan.FromMilliseconds(interval),
				Timeout.InfiniteTimeSpan);
		}
	}
}
