using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.Enums;
using AnimalAdoption.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace AnimalAdoption.UI.Controllers
{
	public class AnimalController : Controller
	{
		private const int PROFILES_PER_PAGE = 5;
		private readonly IAnimalService _animalService;
		private readonly IRequestService _requestService;

		public AnimalController(
				IAnimalService animalService,
				IRequestService requestService
			)
		{
			_animalService = animalService;
			_requestService = requestService;
		}

		[HttpGet]
		[Route("/")]
		[Route("[action]")]
		[AllowAnonymous]
		public async Task<IActionResult> Main()
		{
			return View();
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Feed(int? page)
		{
			var profileResponses = await _animalService.GetAnimalProfiles();

			int pageNumber = (page ?? 1);

			return View(await profileResponses.ToPagedListAsync(pageNumber, PROFILES_PER_PAGE));
		}

		[HttpGet]
		[Route("[action]/{id}")]
		public async Task<IActionResult> Details(Guid? id)
		{
			var animalProfile = await _animalService.GetAnimalProfileById(id.Value);

			ViewBag.Title = "Details";
			return View(animalProfile);
		}

		[HttpGet]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]")]
		public async Task<IActionResult> Requests()
		{
			var requests = await _requestService.GetRequests();

			return View(requests);
		}

		[HttpGet]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[Route("[action]/{id}")]
		public async Task<IActionResult> Delete(Guid? id)
		{
			await _animalService.DeleteAnimalProfile(id);

			return RedirectToAction(nameof(this.Feed));
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> SearchByName(string? searchValue, int? page)
		{
			if (searchValue is null)
			{
				return RedirectToAction(nameof(this.Feed));
			}

			var results = await _animalService.SearchByName(searchValue);

			int pageNumber = (page ?? 1);

			ViewBag.SearchValue = searchValue;

			return View(nameof(this.Feed), await results.ToPagedListAsync(pageNumber, PROFILES_PER_PAGE));
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("[action]")]
		public async Task<IActionResult> Create(AddRequest? addRequest)
		{
			if (await _requestService.AddRequest(addRequest))
			{
				return View("ThanksForCreating");
			}

			return RedirectToAction(nameof(ErrorController.Error), nameof(ErrorController.Error));
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Edit()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("[action]")]
		public async Task<IActionResult> Adopt(Guid? id)
		{
			await _animalService.DeleteAnimalProfile(id);

			return View("ThanksForAdoption");
		}

		[HttpPost]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[ValidateAntiForgeryToken]
		[Route("[action]")]
		public async Task<IActionResult> Approve(Guid? id)
		{
			var succeeded = await _requestService.ApproveRequest(id);

			if (succeeded)
			{
				return RedirectToAction(nameof(this.Requests));
			}

			return RedirectToAction("Error", "Error");
		}

		[HttpPost]
		[Authorize(Roles = $"{nameof(UserTypeOptions.Admin)}")]
		[ValidateAntiForgeryToken]
		[Route("[action]")]
		public async Task<IActionResult> Reject(Guid? id)
		{
			var succeeded = await _requestService.RejectRequest(id);

			if (succeeded)
			{
				return RedirectToAction(nameof(this.Requests));
			}

			return RedirectToAction("Error", "Error");
		}
	}
}
