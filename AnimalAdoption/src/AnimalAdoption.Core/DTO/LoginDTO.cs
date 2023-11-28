using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class LoginDTO
	{
		[Required(ErrorMessage = "Email cannot be blank")]
		[EmailAddress(ErrorMessage = "Email should be valid")]
		[DataType(DataType.Password)]	
		public string? Email { get; set; }

		[Required(ErrorMessage = "Password cannot be blank")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
