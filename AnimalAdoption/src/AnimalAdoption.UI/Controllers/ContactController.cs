using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.Enums;
using AnimalAdoption.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.UI.Controllers
{
	[Route("[controller]")]
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;

		public ContactController(
			IContactService contactService
			)
		{
			_contactService = contactService;
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> UserRequests()
		{
			return View();
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		[Route("[action]")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ContactFormRequest? contactFormRequest)
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]")]
		public async Task<IActionResult> Requests()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]")]
		public async Task<IActionResult> Respond()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]")]
		public async Task<IActionResult> Respond(ContactFormRequest? contactFormRequest)
		{
			return View();
		}
	}
}
