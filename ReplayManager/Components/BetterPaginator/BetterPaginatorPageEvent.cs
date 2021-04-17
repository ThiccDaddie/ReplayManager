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
