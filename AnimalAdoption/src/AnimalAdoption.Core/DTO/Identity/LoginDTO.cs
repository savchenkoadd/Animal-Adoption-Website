using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.Identity
{
	/// <summary>
	/// Represents a data transfer object for user login information.
	/// </summary>
	public class LoginDTO
	{
		/// <summary>
		/// Gets or sets the email address of the user.
		/// </summary>
		[Required(ErrorMessage = "Email cannot be blank")]
		[EmailAddress(ErrorMessage = "Email should be valid")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		/// <summary>
		/// Gets or sets the password for user authentication.
		/// </summary>
		[Required(ErrorMessage = "Password cannot be blank")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
