using AnimalAdoption.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.UI.Controllers
{
	public class AnimalController : Controller
	{
		private readonly IAnimalService _animalService;

		public AnimalController(
				IAnimalService animalService
			)
		{
			_animalService = animalService;
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
	}
}
