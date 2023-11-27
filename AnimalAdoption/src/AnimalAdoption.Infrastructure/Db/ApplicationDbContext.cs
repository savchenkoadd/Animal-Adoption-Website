using AnimalAdoption.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.Infrastructure.Db
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AnimalProfile> AnimalProfiles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AnimalProfile>().ToTable(nameof(AnimalProfiles));
		}
	}
}
