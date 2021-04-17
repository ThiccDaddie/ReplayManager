using Microsoft.EntityFrameworkCore;
using ReplayManager.Shared;
using System.Threading.Tasks;

namespace ReplayManager.DataAccess
{
	public class ReplaysContext : DbContext
	{
		public ReplaysContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<ReplayInfo> Replays { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite(@"Data Source=replays.db");

		public async Task RemoveAllReplays()
		{
			await Database.EnsureCreatedAsync();
			await Database.ExecuteSqlRawAsync("DELETE FROM Replays");
		}
	}
}
