using Microsoft.AspNetCore.Identity;

namespace AnimalAdoption.Core.Domain.IdentityEntities
{
	/// <summary>
	/// Represents an application user derived from IdentityUser with additional properties.
	/// </summary>
	public class ApplicationUser : IdentityUser<Guid>
	{
		/// <summary>
		/// Gets or sets the name of the person associated with the user.
		/// </summary>
		public string? PersonName { get; set; }
	}
}
