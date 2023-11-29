using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.UI.Controllers
{
	[AllowAnonymous]
	public class ErrorController : Controller
	{
		[Route("[action]")]
		public async Task<IActionResult> Error()
		{
			return View();
		}
	}
}
