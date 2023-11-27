namespace AnimalAdoption.Core.DTO
{
	public class AnimalProfileResponse
	{
		public Guid Id { get; set; }

		public int? Age { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		public string? Breed { get; set; }
	}
}
