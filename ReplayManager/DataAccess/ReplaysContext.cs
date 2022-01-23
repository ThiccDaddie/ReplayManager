// <copyright file="ReplaysContext.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using ReplayManager.Models;
using ReplayManager.Models.Pre;

namespace ReplayManager.DataAccess
{
	public sealed class ReplaysContext : DbContext
	{
		public ReplaysContext()
		{
			//Database.EnsureCreated();
		}

		public DbSet<ReplayInfo> Replays { get; set; } = null!;
		public DbSet<PreBattleInfo> PreBattleInfos { get; set; } = null!;

		public DbSet<PreVehicle> PreVehicles { get; set; } = null!;

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
			//modelBuilder
			//	.Entity<ReplayInfo>()
			//	.HasKey(r => r.Path);

			//modelBuilder
			//	.Entity<ReplayInfo>()
			//	.HasOne(r => r.PreBattleInfo)
			//	.WithOne(p => p.ReplayInfo)
			//	.HasForeignKey<PreBattleInfo>(p => p.ReplayInfoId);

			//modelBuilder
			//	.Entity<PreBattleInfo>()
			//	.HasKey(p => p.PreBattleInfoId);

			//modelBuilder
			//	.Entity<PreVehicle>()
			//	.HasKey(v => v.VehicleId);

			
			//modelBuilder
			//	.Ignore<PreBattleInfo>()
			//	.Ignore<PostBattleInfo>();
		}
	}
}
