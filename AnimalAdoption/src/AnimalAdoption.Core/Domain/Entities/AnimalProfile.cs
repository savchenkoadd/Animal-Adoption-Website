namespace AnimalAdoption.Core.Domain.Entities
{
	public class AnimalProfile
	{
		public Guid Id { get; set; }
		public int? Age { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public string? Breed { get; set; }
	}
}
