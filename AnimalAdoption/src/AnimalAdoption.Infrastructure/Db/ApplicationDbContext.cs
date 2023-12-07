using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

			InitializeBindHelper(modelBuilder);

			var contexts = CreateBindingContextList();

			BindingHelper.BindEntitiesToTables(contexts);
		}

		private List<BindingContext> CreateBindingContextList()
		{
			return new List<BindingContext>
			{
				new BindingContext(typeof(AnimalProfile), nameof(AnimalProfiles)),
				new BindingContext(typeof(Request), nameof(Requests)),
				new BindingContext(typeof(ContactForm), nameof(ContactForms))
			};
		}

		private void InitializeBindHelper(ModelBuilder modelBuilder)
		{
			BindingHelper.InitializeBuilder(modelBuilder);
		}
	}
}
