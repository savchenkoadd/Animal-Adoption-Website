using AnimalAdoption.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.Identity
{
	/// <summary>
	/// Represents a data transfer object for user registration information.
	/// </summary>
	public class RegisterDTO
	{
		/// <summary>
		/// Gets or sets the email address of the user.
		/// </summary>
		[Remote(controller: "Account", action: "IsEmailAlreadyRegistered", ErrorMessage = "Email is already in use")]
		[Required(ErrorMessage = "Email can't be blank")]
		[EmailAddress(ErrorMessage = "Email should be in the proper email address format")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the name of the person.
		/// </summary>
		[Required(ErrorMessage = "Name can't be blank")]
		public string PersonName { get; set; }

		/// <summary>
		/// Gets or sets the phone number of the user.
		/// </summary>
		[Phone(ErrorMessage = "Phone should be in the proper phone format")]
		[Required(ErrorMessage = "Phone can't be blank")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		/// <summary>
		/// Gets or sets the password for user authentication.
		/// </summary>
		[Required(ErrorMessage = "Password can't be blank")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the confirmation password for user authentication.
		/// </summary>
		[Required(ErrorMessage = "Confirm Password can't be blank")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Gets or sets the type of user.
		/// </summary>
		public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
	}
}
