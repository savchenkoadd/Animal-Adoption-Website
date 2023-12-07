using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	/// <summary>
	/// Interface defining operations for managing contact forms in a repository.
	/// </summary>
	public interface IContactRepository
	{
		/// <summary>
		/// Creates a new contact form.
		/// </summary>
		/// <param name="contactForm">The contact form to be created.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> Create(ContactForm contactForm);

		/// <summary>
		/// Retrieves a list of all contact forms.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. Returns a list of contact forms or null if none are found.</returns>
		Task<List<ContactForm>?> GetAll();

		/// <summary>
		/// Retrieves contact forms by user identifier.
		/// </summary>
		/// <param name="userId">The unique identifier of the user associated with the contact forms.</param>
		/// <returns>A task representing the asynchronous operation. Returns a list of contact forms for the specified user or null if none are found.</returns>
		Task<List<ContactForm>?> GetByUserId(Guid userId);

		/// <summary>
		/// Responds to a contact form with a specified response.
		/// </summary>
		/// <param name="userId">The unique identifier of the user responding to the contact form.</param>
		/// <param name="formId">The unique identifier of the contact form to respond to.</param>
		/// <param name="response">The response message.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> Respond(Guid userId, Guid formId, string response);
	}
}
