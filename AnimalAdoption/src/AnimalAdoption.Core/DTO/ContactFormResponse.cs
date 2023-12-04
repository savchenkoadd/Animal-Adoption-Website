using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class ContactFormResponse
	{
		[Required(ErrorMessage = "SenderId can't be blank")]
		public Guid SenderId { get; set; }

		[StringLength(1500, ErrorMessage = "Response must not exceed 2000 characters")]
		[Required(ErrorMessage = "Response can't be blank")]
		public string? Response { get; set; }
	}
}
