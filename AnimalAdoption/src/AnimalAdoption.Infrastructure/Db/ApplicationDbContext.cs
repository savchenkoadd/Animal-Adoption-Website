using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.Infrastructure.Db
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AnimalProfile> AnimalProfiles { get; set; }
		public DbSet<Request> Requests { get; set; }
		public DbSet<ContactForm> ContactForms { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AnimalProfile>().ToTable(nameof(AnimalProfiles));
			modelBuilder.Entity<Request>().ToTable(nameof(Requests));
			modelBuilder.Entity<ContactForm>().ToTable(nameof(ContactForms));
		}
	}
}
