using Microsoft.EntityFrameworkCore;
using ReplayManager.Shared;

namespace ReplayManager.DataAccess
{
	public class ReplaysContext : DbContext
	{
		public DbSet<ReplayInfo> Replays { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite(@"Data Source=replays.db");
	}
}
