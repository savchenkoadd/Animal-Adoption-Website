using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.AnimalProfile
{
	/// <summary>
	/// Represents a data transfer object for updating an animal profile.
	/// </summary>
	public class AnimalProfileUpdateRequest
	{
		/// <summary>
		/// Gets or sets the updated age of the animal.
		/// </summary>
		[Range(0, 100)]
		public int? Age { get; set; }

		/// <summary>
		/// Gets or sets the updated name of the animal.
		/// </summary>
		[StringLength(20)]
		public string? Name { get; set; }

		/// <summary>
		/// Gets or sets the updated description of the animal.
		/// </summary>
		[StringLength(1500)]
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the updated URL of the image associated with the animal.
		/// </summary>
		[Url]
		[StringLength(200)]
		public string? ImageUrl { get; set; }

		/// <summary>
		/// Gets or sets the updated breed of the animal.
		/// </summary>
		[StringLength(30)]
		public string? Breed { get; set; }
	}
}
