// <copyright file="ReplaysContext.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReplayManager.Models;

namespace ReplayManager.DataAccess
{
	public sealed class ReplaysContext : DbContext
	{
		public ReplaysContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<ReplayInfo> Replays { get; set; }

		public async Task RemoveAllReplays()
		{
			await Database.EnsureCreatedAsync();
			await Database.ExecuteSqlRawAsync("DELETE FROM Replays");
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override ValueTask DisposeAsync()
		{
			return base.DisposeAsync();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite(@"Data Source=replays.db");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReplayInfo>()
				.HasKey(r => new { r.Directory, r.RelativeFilePath });
		}
	}
}
