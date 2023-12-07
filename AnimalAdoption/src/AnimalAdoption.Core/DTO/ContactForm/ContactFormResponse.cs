using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.ContactForm
{
	/// <summary>
	/// Represents a data transfer object for displaying the response to a contact form.
	/// </summary>
	public class ContactFormResponse
	{
		/// <summary>
		/// Gets or sets the unique identifier of the contact form.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of the sender.
		/// </summary>
		public Guid SenderId { get; set; }

		/// <summary>
		/// Gets or sets the email address of the sender.
		/// </summary>
		public string? SenderEmail { get; set; }

		/// <summary>
		/// Gets or sets the subject of the contact form.
		/// </summary>
		public string? Subject { get; set; }

		/// <summary>
		/// Gets or sets the description of the contact form.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the response to the contact form.
		/// </summary>
		public string? Response { get; set; }
	}
}
