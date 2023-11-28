using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class RegisterDTO
	{
		[Required(ErrorMessage = "Email can't be blank")]
		[EmailAddress(ErrorMessage = "Email should be in the proper email address format")]
		[Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already in use")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Name can't be blank")]
		public string PersonName { get; set; }

		[Phone(ErrorMessage = "Phone should be in the proper Phone format")]
		[Required(ErrorMessage = "Phone can't be blank")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Password can't be blank")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password can't be blank")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}
