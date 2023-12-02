using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class AddRequest
	{
		[Range(0, 100)]
		public int? Age { get; set; }

		[StringLength(20)]
		public string? Name { get; set; }

		[StringLength(1500)]
		public string? Description { get; set; }

		[StringLength(200)]
		[Url]
		public string? ImageUrl { get; set; }

		[StringLength(30)]
		public string? Breed { get; set; }
	}
}
