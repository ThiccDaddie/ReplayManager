// <copyright file="BetterPaginatorPageEvent.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using MatBlazor;

namespace ReplayManager.Components
{
	public class BetterPaginatorPageEvent
	{
		public BetterPaginatorPageEvent(int pageIndex, MatPageSizeOption sizeOption, int length)
		{
			PageIndex = pageIndex;
			SizeOption = sizeOption;
			Length = length;
		}

		public int PageIndex { get; set; }

		public MatPageSizeOption SizeOption { get; set; }

		public int Length { get; set; }
	}
}
