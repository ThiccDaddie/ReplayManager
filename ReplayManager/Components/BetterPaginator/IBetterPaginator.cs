using MatBlazor;
using System.Collections.Generic;

namespace ReplayManager.Components
{
	public interface IBetterPaginator
	{
		string PageSizeText { get; set; }
		int PageSize { get; }
		IReadOnlyList<MatPageSizeOption> PageSizeOptions { get; set; }
	}
}
