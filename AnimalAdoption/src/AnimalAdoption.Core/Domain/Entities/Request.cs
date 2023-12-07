using AnimalAdoption.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Domain.Entities
{
	/// <summary>
	/// Represents a request entity for adopting an animal.
	/// </summary>
	public class Request
	{
		/// <summary>
		/// Gets or sets the unique identifier of the associated animal.
		/// </summary>
		[Key]
		public Guid AnimalId { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of the user making the request.
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Gets or sets the age of the animal.
		/// </summary>
		public int? Age { get; set; }

		/// <summary>
		/// Gets or sets the status of the adoption request.
		/// </summary>
		public RequestStatus Status { get; set; } = RequestStatus.InProcess;

		/// <summary>
		/// Gets or sets the name of the animal.
		/// </summary>
		[StringLength(20)]
		public string? Name { get; set; }

		/// <summary>
		/// Gets or sets the description of the adoption request.
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
