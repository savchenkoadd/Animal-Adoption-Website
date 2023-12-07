using Microsoft.AspNetCore.Identity;

namespace AnimalAdoption.Core.Domain.IdentityEntities
{
	/// <summary>
	/// Represents an application role derived from IdentityRole with a Guid as the key.
	/// </summary>
	public class ApplicationRole : IdentityRole<Guid>
	{
	}
}
