using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class ContactFormResponse
	{
		[Required(ErrorMessage = "SenderId can't be blank")]
		public Guid SenderId { get; set; }

		[StringLength(50, ErrorMessage = "Sender email must not exceed 50 characters")]
		[EmailAddress(ErrorMessage = "Email must be valid")]
		[Required(ErrorMessage = "Sender email can't be blank")]
		public string? SenderEmail { get; set; }

		[StringLength(50, ErrorMessage = "Subject must not exceed 50 characters")]
		[Required(ErrorMessage = "Subject can't be blank")]
		public string? Subject { get; set; }

		[StringLength(1500, ErrorMessage = "Description must not exceed 1500 characters")]
		[Required(ErrorMessage = "Description can't be blank")]
		public string? Description { get; set; }

		[StringLength(1500, ErrorMessage = "Response must not exceed 2000 characters")]
		[Required(ErrorMessage = "Response can't be blank")]
		public string? Response { get; set; }
	}
}
