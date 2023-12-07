using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	/// <summary>
	/// Interface defining operations for managing adoption requests in a repository.
	/// </summary>
	public interface IRequestRepository
	{
		/// <summary>
		/// Adds a new adoption request.
		/// </summary>
		/// <param name="request">The adoption request to be added.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> AddRequest(Request request);

		/// <summary>
		/// Deletes an adoption request by its unique identifier.
		/// </summary>
		/// <param name="requestId">The unique identifier of the adoption request to be deleted.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> DeleteRequest(Guid requestId);

		/// <summary>
		/// Retrieves an adoption request by its unique identifier.
		/// </summary>
		/// <param name="requestId">The unique identifier of the adoption request to retrieve.</param>
		/// <returns>A task representing the asynchronous operation. Returns the requested adoption request or null if not found.</returns>
		Task<Request?> GetRequest(Guid requestId);

		/// <summary>
		/// Retrieves a list of all adoption requests.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. Returns a list of adoption requests or null if none are found.</returns>
		Task<List<Request>?> GetAllRequests();

		/// <summary>
		/// Retrieves adoption requests by user identifier.
		/// </summary>
		/// <param name="userId">The unique identifier of the user associated with the adoption requests.</param>
		/// <returns>A task representing the asynchronous operation. Returns a list of adoption requests for the specified user or null if none are found.</returns>
		Task<List<Request>?> GetRequestsByUserId(Guid userId);

		/// <summary>
		/// Updates an existing adoption request.
		/// </summary>
		/// <param name="requestId">The unique identifier of the adoption request to be updated.</param>
		/// <param name="updateRequest">The updated adoption request data.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> UpdateRequest(Guid requestId, Request updateRequest);
	}
}
