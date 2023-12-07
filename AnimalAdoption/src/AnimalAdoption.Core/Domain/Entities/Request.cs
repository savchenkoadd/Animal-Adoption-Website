using AnimalAdoption.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Domain.Entities
{
	public class Request
	{
		[Key]
		public Guid AnimalId { get; set; }

		public Guid UserId { get; set; }

		public int? Age { get; set; }

		public RequestStatus Status { get; set; } = RequestStatus.InProcess;

		[StringLength(20)]
		public string? Name { get; set; }

		[StringLength(1500)]
		public string? Description { get; set; }

		[StringLength(200)]
		public string? ImageUrl { get; set; }

		[StringLength(30)]
		public string? Breed { get; set; }
	}
}
