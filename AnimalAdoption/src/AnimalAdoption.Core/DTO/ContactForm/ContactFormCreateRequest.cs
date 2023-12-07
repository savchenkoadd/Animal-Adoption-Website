using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.ContactForm
{
	/// <summary>
	/// Represents a data transfer object for creating a contact form.
	/// </summary>
	public class ContactFormCreateRequest
	{
		/// <summary>
		/// Gets or sets the unique identifier of the sender.
		/// </summary>
		[Required(ErrorMessage = "SenderId can't be blank")]
		public Guid SenderId { get; set; }

		/// <summary>
		/// Gets or sets the email address of the sender.
		/// </summary>
		[Required(ErrorMessage = "SenderEmail can't be blank")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string? SenderEmail { get; set; }

		/// <summary>
		/// Gets or sets the subject of the contact form.
		/// </summary>
		[StringLength(50, ErrorMessage = "Subject must not exceed 50 characters")]
		[Required(ErrorMessage = "Subject can't be blank")]
		public string? Subject { get; set; }

		/// <summary>
		/// Gets or sets the description of the contact form.
		/// </summary>
		[StringLength(1500, ErrorMessage = "Description must not exceed 1500 characters")]
		[Required(ErrorMessage = "Description can't be blank")]
		public string? Description { get; set; }
	}
}
