using System.Data.Entity;

namespace MaintJournal.Models
{
	public class JournalDbContext : DbContext
	{
		public JournalDbContext(string dbConnection) : base(dbConnection) { }

		public DbSet<Journal> Journals { get; set; }
	}
}
