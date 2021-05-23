using System;
using System.Linq;
using System.Linq.Expressions;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using ReplayManager.Models;

namespace ReplayManager.Components
{
	public partial class FilterPanel
	{

		[Parameter]
		public Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>> Filter { get; set; }

		[Parameter]
		public EventCallback<Func<IQueryable<ReplayInfo>, IQueryable<ReplayInfo>>> FilterChanged { get; set; }

		public async void OnFilter(MatSortChangedEvent e)
		{

			await FilterChanged.InvokeAsync(replays => ApplyFilter(replays, e));
		}

		private static IQueryable<ReplayInfo> ApplyFilter(IQueryable<ReplayInfo> replays, MatSortChangedEvent e)
		{

			if (e.Direction == MatSortDirection.None)
			{
				return replays;
			}

			var entityType = typeof(ReplayInfo);
			var propertyName = nameof(ReplayInfo.DateTime);

			var propertyInfo = entityType.GetProperty(propertyName);

			if (propertyInfo.DeclaringType != entityType)
			{
				propertyInfo = propertyInfo.DeclaringType.GetProperty(propertyName);
			}

			if (propertyInfo is null)
			{
				return replays;
			}

			var arg = Expression.Parameter(entityType, "r");
			var property = Expression.MakeMemberAccess(arg, propertyInfo);
			var selector = Expression.Lambda(property, arg);

			string method = e.Direction == MatSortDirection.Asc ? "OrderByDescending" : "OrderBy";
			var genericMethod = typeof(Queryable).GetMethods()
										.Where(m => m.Name == method && m.IsGenericMethodDefinition && m.GetParameters().Length == 2)
										.Single()
										.MakeGenericMethod(entityType, propertyInfo.PropertyType);

			return (IOrderedQueryable<ReplayInfo>)genericMethod.Invoke(genericMethod, new object[] { replays, selector });
		}
	}
}
