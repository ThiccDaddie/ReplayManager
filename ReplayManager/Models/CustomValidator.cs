using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;

namespace ReplayManager.Models
{
	public class CustomValidator : ComponentBase
	{
		private ValidationMessageStore _messageStore;

		[CascadingParameter]
		private EditContext CurrentEditContext { get; set; }

		protected override void OnInitialized()
		{
			if (CurrentEditContext is null)
			{
				throw new InvalidOperationException(
					@$"{nameof(CustomValidator)} requires a cascading
					parameter of type {nameof(EditContext)}.
					For example, you can use {nameof(CustomValidator)}
					inside an {nameof(EditForm)}.");
			}

			_messageStore = new(CurrentEditContext);

			CurrentEditContext.OnValidationRequested += (s, e) =>
				_messageStore.Clear();
			CurrentEditContext.OnFieldChanged += (s, e) =>
				_messageStore.Clear(e.FieldIdentifier);
		}

		public void DisplayError(string key, List<string> errors)
		{
			_messageStore.Add(CurrentEditContext.Field(key), errors);

			CurrentEditContext.NotifyValidationStateChanged();
		}

		public void DisplayErrors(Dictionary<string, List<string>> errors)
		{
			foreach (var err in errors)
			{
				_messageStore.Add(CurrentEditContext.Field(err.Key), err.Value);
			}

			CurrentEditContext.NotifyValidationStateChanged();
		}

		public void ClearErrors()
		{
			_messageStore.Clear();
			CurrentEditContext.NotifyValidationStateChanged();
		}
	}
}
