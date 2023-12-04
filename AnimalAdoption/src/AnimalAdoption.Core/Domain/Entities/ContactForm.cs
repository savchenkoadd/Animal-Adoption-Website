using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Domain.Entities
{
	public class ContactForm
	{
		[Key]
		public Guid Id { get; set; }

		public Guid SenderId { get; set; }

		[StringLength(50)]
		public string? SenderEmail { get; set; }

		[StringLength(50)]
		public string? Subject { get; set; }

		[StringLength(1500)]
		public string? Description { get; set; }

		[StringLength(2000)]
		public string? Response { get; set; }
	}
}
