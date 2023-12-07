using AnimalAdoption.Core.DTO.Request;

namespace AnimalAdoption.Core.ServiceContracts
{
    /// <summary>
    /// Service contract defining operations for managing user create requests.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Adds a new adoption request.
        /// </summary>
        /// <param name="request">The request containing data for creating the user create request.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
        Task<bool> AddRequest(AddRequest? request);

        /// <summary>
        /// Rejects an adoption request.
        /// </summary>
        /// <param name="requestId">The unique identifier of the request to be rejected.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
        Task<bool> RejectRequest(Guid? requestId);

        /// <summary>
        /// Approves an adoption request.
        /// </summary>
        /// <param name="requestId">The unique identifier of the adoption request to be approved.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
        Task<bool> ApproveRequest(Guid? requestId);

        /// <summary>
        /// Retrieves a list of user create requests for administrators.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a list of user requests.</returns>
        Task<List<RequestResponse>> GetRequestsForAdmin();

        /// <summary>
        /// Retrieves user requests by user identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user associated with the adoption requests.</param>
        /// <returns>A task representing the asynchronous operation. Returns a list of user requests for the specified user.</returns>
        Task<List<RequestResponse>> GetRequestsByUserId(Guid? userId);
    }
}
