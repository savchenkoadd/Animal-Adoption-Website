﻿using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.Enums;
using AnimalAdoption.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.UI.Controllers
{
	[Route("[controller]")]
	public class ContactController : Controller
	{
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
		public async Task<IActionResult> UserRequests()
		{
			var currentUserId = (await _userManager.GetUserAsync(User)).Id;

			var responses = await _contactService.GetByUserId(currentUserId);

			return View(responses);
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
		public async Task<IActionResult> Create(ContactFormCreateRequest? contactFormRequest)
		{
			if (await _contactService.Create(contactFormRequest))
			{
				return RedirectToAction(nameof(this.UserRequests));
			}

			return RedirectToAction(nameof(ErrorController.Error), nameof(ErrorController.Error));
		}

		[HttpGet]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]")]
		public async Task<IActionResult> Requests()
		{
			var forms = await _contactService.GetAll();

			return View(forms);
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
		public async Task<IActionResult> Respond(ContactFormCreateRequest? contactFormRequest)
		{
			if (await _contactService.Respond(contactFormRequest.SenderId, contactFormRequest.Id, contactFormRequest.Response))
			{
				return RedirectToAction(nameof(this.UserRequests));
			}

			return RedirectToAction(nameof(ErrorController.Error), nameof(ErrorController.Error));
		}
	}
}
