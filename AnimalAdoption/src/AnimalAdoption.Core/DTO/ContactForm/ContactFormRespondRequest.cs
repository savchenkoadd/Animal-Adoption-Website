using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.ContactForm
{
	public class ContactFormRespondRequest
	{
		[Required]
		public Guid? Id { get; set; }

		[Required]
		public Guid? SenderId { get; set; }

		[Required(ErrorMessage = "Response can't be blank")]
		[StringLength(2000, ErrorMessage = "Response must not exceed 2000 characters")]
		public string? Response { get; set; }
	}
}
