using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.UI.Controllers
{
	public class AnimalController : Controller
	{
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
		public async Task<IActionResult> Feed()
		{
			var profileResponses = await _animalService.GetAnimalProfiles();

			return View(profileResponses);
		}

		[HttpGet]
		[Route("[action]/{id}")]
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id is null)
			{
				return RedirectToAction("Error", "Error");
			}

			var animalProfile = await _animalService.GetAnimalProfileById(id.Value);

			ViewBag.Title = "Details";
			return View(animalProfile);
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Requests()
		{
			var requests = await _requestService.GetRequests();

			return View(requests);
		}

		[HttpPost]
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
