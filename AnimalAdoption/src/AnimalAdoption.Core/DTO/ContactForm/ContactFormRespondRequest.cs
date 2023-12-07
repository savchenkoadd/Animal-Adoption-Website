using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.ContactForm
{
	/// <summary>
	/// Represents a data transfer object for responding to a contact form.
	/// </summary>
	public class ContactFormRespondRequest
	{
		/// <summary>
		/// Gets or sets the unique identifier of the contact form.
		/// </summary>
		[Required(ErrorMessage = "Id can't be blank")]
		public Guid? Id { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of the sender.
		/// </summary>
		[Required(ErrorMessage = "SenderId can't be blank")]
		public Guid? SenderId { get; set; }

		/// <summary>
		/// Gets or sets the response to the contact form.
		/// </summary>
		[Required(ErrorMessage = "Response can't be blank")]
		[StringLength(2000, ErrorMessage = "Response must not exceed 2000 characters")]
		public string? Response { get; set; }
	}
}
