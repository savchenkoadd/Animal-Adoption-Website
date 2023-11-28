using Microsoft.AspNetCore.Identity;

namespace AnimalAdoption.Core.Domain.IdentityEntities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string? PersonName { get; set; }
	}
}
