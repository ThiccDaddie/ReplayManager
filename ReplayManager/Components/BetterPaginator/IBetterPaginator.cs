// <copyright file="IBetterPaginator.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Collections.Generic;
using MatBlazor;

namespace ReplayManager.Components
{
	public interface IBetterPaginator
	{
		string PageSizeText { get; set; }

		int PageSize { get; }

		IReadOnlyList<MatPageSizeOption> PageSizeOptions { get; set; }
	}
}
