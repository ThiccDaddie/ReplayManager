using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace ReplayManager.Services
{
	public class BrowserResizeService
	{
		public static event Func<Task> OnResize;

		[JSInvokable]
		public static async Task OnBrowserResize()
		{
			if (OnResize is not null)
			{
				await OnResize.Invoke();
			}
		}

		public static async Task<int> GetInnerHeight(IJSRuntime jSRuntime, ElementReference reference)
		{
			return await jSRuntime.InvokeAsync<int>("browserResize.getInnerHeight", reference);
		}

		public static async Task<int> GetInnerWidth(IJSRuntime jSRuntime, ElementReference reference)
		{
			return await jSRuntime.InvokeAsync<int>("browserResize.getInnerWidth", reference);
		}
	}
}
