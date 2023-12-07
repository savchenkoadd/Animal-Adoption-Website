using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.AnimalProfile
{
	/// <summary>
	/// Represents a data transfer object for displaying an animal profile.
	/// </summary>
	public class AnimalProfileResponse
	{
		/// <summary>
		/// Gets or sets the unique identifier of the animal profile.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the age of the animal.
		/// </summary>
		[Range(0, 100, ErrorMessage = "Age must be in the range of 0 to 100")]
		[Required(ErrorMessage = "Age can't be empty")]
		public int? Age { get; set; }

		/// <summary>
		/// Gets or sets the name of the animal.
		/// </summary>
		[StringLength(20, ErrorMessage = "Name length must not exceed 20 characters")]
		[Required(ErrorMessage = "Name can't be empty")]
		public string? Name { get; set; }

		/// <summary>
		/// Gets or sets the description of the animal.
		/// </summary>
		[StringLength(1500, ErrorMessage = "Description length must not exceed 1500 characters")]
		[Required(ErrorMessage = "Description can't be empty")]
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the URL of the image associated with the animal.
		/// </summary>
		[Url(ErrorMessage = "Image URL must be a valid URL")]
		[StringLength(200, ErrorMessage = "Image URL length must not exceed 200 characters")]
		[Required(ErrorMessage = "Image URL can't be empty")]
		public string? ImageUrl { get; set; }

		/// <summary>
		/// Gets or sets the breed of the animal.
		/// </summary>
		[StringLength(30, ErrorMessage = "Breed length must not exceed 30 characters")]
		[Required(ErrorMessage = "Breed can't be empty")]
		public string? Breed { get; set; }
	}
}
