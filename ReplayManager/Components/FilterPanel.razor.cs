using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using ReplayManager.Models;

namespace ReplayManager.Components
{
	public partial class FilterPanel
	{
		[Parameter]
		public Expression FilterExpression { get; set; }

		[Parameter]
		public EventCallback<Expression> FilterExpressionChanged { get; set; }

		public async void OnFilter(MatSortChangedEvent e)
		{
			Expression tree = null;
			if (e.Direction != MatSortDirection.None)
			{
				ParameterExpression prm = Expression.Parameter(typeof(ReplayInfo), "r");
				Expression orderByProperty = Expression.Property(prm, nameof(ReplayInfo.DateTime));

				Expression<Func<ReplayInfo, DateTime>> orderByExpression = replay => replay.DateTime;

				string method = e.Direction == MatSortDirection.Asc ? "OrderBy" : "OrderByDescending";
				MethodInfo methodInfo = typeof(Queryable)
					.GetMethods()
					.Where(m => m.Name == method && m.GetParameters().Length == 2)
					.Single()
					.MakeGenericMethod(typeof(ReplayInfo), typeof(DateTime));

				tree = Expression.Call(
					methodInfo,
					Expression.Parameter(typeof(IQueryable<ReplayInfo>)),
					orderByExpression);
			}

			await FilterExpressionChanged.InvokeAsync(tree);
		}
	}
}
