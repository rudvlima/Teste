using Microsoft.EntityFrameworkCore;
using knewinAPI.Models;

namespace knewinAPI.Models
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options)
        {
        }


        public DbSet<knewinAPI.Models.Cidade> Cidade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Cidade>()
				.Property(e => e.Fronteira)
				.HasConversion(
					v => string.Join(',', v),
					v => v.Split(',',System.StringSplitOptions.RemoveEmptyEntries)
				);
		}
    }
}