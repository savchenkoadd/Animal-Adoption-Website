namespace AnimalAdoption.Core.Enums
{
	/// <summary>
	/// Represents the status of an adoption request.
	/// </summary>
	public enum RequestStatus
	{
		/// <summary>
		/// The adoption request is in the process of being reviewed.
		/// </summary>
		InProcess,

		/// <summary>
		/// The adoption request has been approved.
		/// </summary>
		Approved,

		/// <summary>
		/// The adoption request has been rejected.
		/// </summary>
		Rejected
	}
}
