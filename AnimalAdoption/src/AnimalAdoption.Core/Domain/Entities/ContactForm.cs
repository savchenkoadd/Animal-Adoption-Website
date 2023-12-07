using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Domain.Entities
{
	/// <summary>
	/// Represents a contact form entity used for communication.
	/// </summary>
	public class ContactForm
	{
		/// <summary>
		/// Gets or sets the unique identifier for the contact form.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier of the sender.
		/// </summary>
		public Guid SenderId { get; set; }

		/// <summary>
		/// Gets or sets the email address of the sender.
		/// </summary>
		[StringLength(50)]
		public string? SenderEmail { get; set; }

		/// <summary>
		/// Gets or sets the subject of the contact form.
		/// </summary>
		[StringLength(50)]
		public string? Subject { get; set; }

		/// <summary>
		/// Gets or sets the description provided in the contact form.
		/// </summary>
		[StringLength(1500)]
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the response to the contact form.
		/// </summary>
		[StringLength(2000)]
		public string? Response { get; set; }
	}
}
