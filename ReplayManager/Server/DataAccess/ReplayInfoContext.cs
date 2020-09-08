using Microsoft.EntityFrameworkCore;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Server.DataAccess
{
	public class ReplayInfoContext : DbContext
	{
		public DbSet<ReplayInfo> ReplayInfos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite("Data Source=replaymanager.db");
	}
}
