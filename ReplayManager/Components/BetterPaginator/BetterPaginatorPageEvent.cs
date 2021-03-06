﻿// <copyright file="BetterPaginatorPageEvent.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using MatBlazor;

namespace ReplayManager.Components
{
	public class BetterPaginatorPageEvent
	{
		public int PageIndex { get; set; }

		public MatPageSizeOption SizeOption { get; set; }

		public int Length { get; set; }
	}
}
