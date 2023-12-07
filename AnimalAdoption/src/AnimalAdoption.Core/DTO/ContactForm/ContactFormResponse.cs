using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.ContactForm
{
	public class ContactFormResponse
	{
		public Guid Id { get; set; }

		public Guid SenderId { get; set; }

		public string? SenderEmail { get; set; }

		public string? Subject { get; set; }

		public string? Description { get; set; }

		public string? Response { get; set; }
	}
}
