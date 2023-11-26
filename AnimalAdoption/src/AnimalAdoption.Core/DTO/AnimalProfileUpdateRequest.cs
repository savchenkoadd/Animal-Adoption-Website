using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class AnimalProfileUpdateRequest
	{
		[Range(0, 100)]
		public int? Age { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		[Url]
		public string? ImageUrl { get; set; }
		public string? Breed { get; set; }
	}
}
