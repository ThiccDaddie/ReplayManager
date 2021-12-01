// <copyright file="BetterPaginator.razor.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace ReplayManager.Components
{
	public partial class BetterPaginator : BaseMatDomComponent, IBetterPaginator
	{
		public const string PageLabelDefault = "Page:";

		private static readonly IReadOnlyList<MatPageSizeOption> DefaultPageSizeOptions = new MatPageSizeOption[]
		{
			new MatPageSizeOption(5),
			new MatPageSizeOption(10),
			new MatPageSizeOption(25),
			new MatPageSizeOption(50),
			new MatPageSizeOption(100),
			new MatPageSizeOption(int.MaxValue, "*"),
		};

		[Parameter]
		public EventCallback<BetterPaginatorPageEvent> Page { get; set; }

		[Parameter]
		public string Label { get; set; } = "Items per Page:";

		[Parameter]
		public string PageLabel { get; set; } = PageLabelDefault;

		[Parameter]
		public string? PageSizeText { get; set; }

		public int? PageSize
		{
			get
			{
				if (PageSizeText == null)
				{
					return null;
				}

				return GetPageSizeFromPageSizeText(PageSizeText);
			}
		}

		public MatPageSizeOption? PageSizeOption
		{
			get
			{
				return PageSizeOptions.FirstOrDefault(option => option.Text == PageSizeText);
			}
		}

		[Parameter]
		public int Length { get; set; }

		[Parameter]
		public int PageIndex { get; set; }

		[Parameter]
		public IReadOnlyList<MatPageSizeOption> PageSizeOptions { get; set; } = DefaultPageSizeOptions;

		[Parameter]
		public EventCallback<int> PageIndexChanged { get; set; }

		protected int TotalPages { get; set; }

		public static void OnInitializedStatic(IBetterPaginator paginator)
		{
			if (paginator.PageSize == 0 && paginator.PageSizeOptions != null && paginator.PageSizeOptions.Count > 0)
			{
				paginator.PageSizeText = paginator.PageSizeOptions[0].Value.ToString();
			}
		}

		public void Update()
		{
			// Length = ParentDataTable?.ItemsComponent?.Length() ?? Length;
			TotalPages = CalculateTotalPages(PageSize);
		}

		public async Task NavigateToPage(MatPaginatorAction direction, string pageSizeText)
		{
			int pageSize = GetPageSizeFromPageSizeText(pageSizeText);

			var pageSizeChanged = pageSize != PageSize;
			var totalPages = CalculateTotalPages(pageSize);
			var page = PageIndex;

			if (pageSizeChanged)
			{
				try
				{
					if (PageSize != null)
					{
						page = (PageIndex * PageSize.Value) / pageSize;
					}
					else
					{
						page = 0;
					}
				}
				catch
				{
					page = 0;
				}
			}

			try
			{
				checked
				{
					switch (direction)
					{
						case MatPaginatorAction.Default:
							break;
						case MatPaginatorAction.First:
							page = 0;
							break;
						case MatPaginatorAction.Previous:
							page--;
							break;
						case MatPaginatorAction.Next:
							page++;
							break;
						case MatPaginatorAction.Last:
							page = totalPages;
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
					}
				}
			}
			catch (Exception)
			{
			}

			if (page < 0)
			{
				page = 0;
			}

			if (totalPages - page <= 1)
			{
				page = TotalPages == 0 ? 0 : totalPages - 1;
			}

			if (PageIndex != page || pageSize != PageSize)
			{
				PageIndex = page;
				PageSizeText = pageSizeText;
				if (PageSizeOption != null)
				{
					await Page.InvokeAsync(new BetterPaginatorPageEvent(page, PageSizeOption, Length));
				}
			}
		}

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
			Update();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			OnInitializedStatic(this);

			this.Update();
		}

		protected int CalculateTotalPages(int? pageSize)
		{
			if (pageSize == null)
			{
				return 0;
			}

			if (pageSize == 0)
			{
				return int.MaxValue;
			}

			return Math.Max(0, (int)Math.Ceiling((decimal)Length / pageSize.Value));
		}

		protected async Task PageSizeChangedHandler(string key)
		{
			await NavigateToPage(MatPaginatorAction.Default, key);
		}

		private int GetPageSizeFromPageSizeText(string text)
		{
			MatPageSizeOption? sizeOption = PageSizeOptions.FirstOrDefault(option => option.Text == text);
			return sizeOption is not null
				? sizeOption.Value
				: int.Parse(text);
		}
	}
}
