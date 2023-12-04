using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoption.Core.DTO
{
	public class ContactFormRequest
	{
		[Required(ErrorMessage = "SenderId can't be blank")]
		public Guid SenderId { get; set; }

		[StringLength(50, ErrorMessage = "Sender email must not exceed 50 characters")]
		[EmailAddress(ErrorMessage = "Email must be valid")]
		[Required(ErrorMessage = "Sender email can't be blank")]
		public string? SenderEmail { get; set; }

		[StringLength(50, ErrorMessage = "Subject must not exceed 50 characters")]
		[Required(ErrorMessage = "Subject can't be blank")]
		public string? Subject { get; set; }

		[StringLength(1500, ErrorMessage = "Description must not exceed 1500 characters")]
		[Required(ErrorMessage = "Description can't be blank")]
		public string? Description { get; set; }
	}
}
