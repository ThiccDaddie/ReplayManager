// <copyright file="CustomValidator.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ReplayManager.Models
{
	public class CustomValidator : ComponentBase
	{
		private ValidationMessageStore messageStore;

		[CascadingParameter]
		private EditContext CurrentEditContext { get; set; }

		public void DisplayError(string key, List<string> errors)
		{
			messageStore.Add(CurrentEditContext.Field(key), errors);

			CurrentEditContext.NotifyValidationStateChanged();
		}

		public void DisplayErrors(Dictionary<string, List<string>> errors)
		{
			foreach (var err in errors)
			{
				messageStore.Add(CurrentEditContext.Field(err.Key), err.Value);
			}

			CurrentEditContext.NotifyValidationStateChanged();
		}

		public void ClearErrors()
		{
			messageStore.Clear();
			CurrentEditContext.NotifyValidationStateChanged();
		}

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

			messageStore = new(CurrentEditContext);

			CurrentEditContext.OnValidationRequested += (s, e) =>
				messageStore.Clear();
			CurrentEditContext.OnFieldChanged += (s, e) =>
				messageStore.Clear(e.FieldIdentifier);
		}
	}
}
