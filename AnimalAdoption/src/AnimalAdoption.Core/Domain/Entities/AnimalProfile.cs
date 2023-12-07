using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Domain.Entities
{
	/// <summary>
	/// Represents an animal profile for adoption.
	/// </summary>
	public class AnimalProfile
	{
		/// <summary>
		/// Gets or sets the unique identifier for the animal profile.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the age of the animal.
		/// </summary>
		public int? Age { get; set; }

		/// <summary>
		/// Gets or sets the name of the animal.
		/// </summary>
		[StringLength(20)]
		public string? Name { get; set; }

		/// <summary>
		/// Gets or sets the description of the animal.
		/// </summary>
		[StringLength(1500)]
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the URL of the image associated with the animal.
		/// </summary>
		[StringLength(200)]
		public string? ImageUrl { get; set; }

		/// <summary>
		/// Gets or sets the breed of the animal.
		/// </summary>
		[StringLength(30)]
		public string? Breed { get; set; }
	}
}
