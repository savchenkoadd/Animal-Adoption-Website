using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
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
		public async Task<IActionResult> Main()
		{
			return View();
		}

		[HttpGet]
		[Route("/feed")]
		public async Task<IActionResult> Feed()
		{
			var profileResponses = await _animalService.GetAnimalProfiles();

			return View(profileResponses);
		}

		[HttpGet]
		[Route("/create")]
		public async Task<IActionResult> CreateProfile()
		{
			return View();
		}

		[HttpPost]
		[Route("/create")]
		public async Task<IActionResult> CreateProfile(AnimalProfileAddRequest animalProfileAddRequest)
		{
			await _animalService.CreateAnimalProfile(animalProfileAddRequest);

			return View("Created");
		}
	}
}
