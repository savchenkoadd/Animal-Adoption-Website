using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Core.DTO.ContactForm;
using AnimalAdoption.Core.Enums;
using AnimalAdoption.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace AnimalAdoption.UI.Controllers
{
    [Route("[controller]")]
	public class ContactController : Controller
	{
		private readonly int FORMS_PER_PAGE = 3;
		private readonly IContactService _contactService;
		private readonly UserManager<ApplicationUser> _userManager;

		public ContactController(
				IContactService contactService,
				UserManager<ApplicationUser> userManager
			)
		{
			_contactService = contactService;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Create()
		{
			var currentUser = await _userManager.GetUserAsync(User);

			ViewBag.UserId = currentUser.Id;
			ViewBag.SenderEmail = currentUser.Email;

			return View();
		}

		[HttpPost]
		[Route("[action]")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ContactFormCreateRequest? contactFormRequest)
		{
			if (await _contactService.Create(contactFormRequest))
			{
				return RedirectToAction(nameof(this.Requests));
			}

			return RedirectToAction(nameof(ErrorController.Error), nameof(ErrorController.Error));
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Requests(int? page)
		{
			List<ContactFormResponse> forms;

			if (User.IsInRole($"{nameof(UserTypeOptions.User)}"))
			{
				var currentUser = await _userManager.GetUserAsync(User);

				forms = await _contactService.GetByUserId(currentUser.Id);
			}
			else
			{
				forms = await _contactService.GetAll();
			}


			int pageNumber = (page ?? 1);

			return View(await forms.ToPagedListAsync(pageNumber, FORMS_PER_PAGE));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]")]
		public async Task<IActionResult> Respond(ContactFormRespondRequest? request)
		{
			if (await _contactService.Respond(request))
			{
				return RedirectToAction(nameof(this.Requests));
			}

			return RedirectToAction(nameof(ErrorController.Error), nameof(ErrorController.Error));
		}
	}
}
