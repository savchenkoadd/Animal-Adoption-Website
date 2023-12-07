using AnimalAdoption.Core.DTO.ContactForm;

namespace AnimalAdoption.Core.ServiceContracts
{
	/// <summary>
	/// Service contract defining operations for managing contact forms.
	/// </summary>
	public interface IContactService
	{
		/// <summary>
		/// Creates a new contact form.
		/// </summary>
		/// <param name="contactFormRequest">The request containing data for creating the contact form.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> Create(ContactFormCreateRequest? contactFormRequest);

		/// <summary>
		/// Retrieves a list of all contact forms.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. Returns a list of contact forms.</returns>
		Task<List<ContactFormResponse>> GetAll();

		/// <summary>
		/// Retrieves contact forms by user identifier.
		/// </summary>
		/// <param name="userId">The unique identifier of the user associated with the contact forms.</param>
		/// <returns>A task representing the asynchronous operation. Returns a list of contact forms for the specified user.</returns>
		Task<List<ContactFormResponse>> GetByUserId(Guid? userId);

		/// <summary>
		/// Adds a response to a contact form.
		/// </summary>
		/// <param name="request">The request containing data for responding to the contact form.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> Respond(ContactFormRespondRequest? request);
	}
}
