using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.UI.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
				UserManager<ApplicationUser> userManager,
				SignInManager<ApplicationUser> signInManager
			)
        {
            _userManager = userManager;
			_signInManager = signInManager;
        }

        [HttpGet]
		[Route("/[action]")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[Route("/[action]")]
		public async Task<IActionResult> Register(RegisterDTO registerDTO)  
		{
			if (ModelState.IsValid == false)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

				return View(registerDTO);
			}

			ApplicationUser user = new ApplicationUser()
			{
				Email = registerDTO.Email,
				PhoneNumber = registerDTO.Phone,
				UserName = registerDTO.Email,
				PersonName = registerDTO.PersonName,
			};

			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

			if (result.Succeeded)
			{
				//Sign in
				await _signInManager.SignInAsync(user, isPersistent: false);

				return RedirectToAction(nameof(AnimalController.Main), "Animal");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("Register", error.Description);
			}

			return View(registerDTO);
		}

		[HttpGet]
		[Route("/[action]")]
		public async Task<IActionResult> Login()
		{
			return View();
		}

		[HttpPost]
		[Route("/[action]")]
		public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

				return View(loginDTO);
			}

			var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: true);

			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
				{
					return LocalRedirect(ReturnUrl);
				}

				return RedirectToAction(nameof(AnimalController.Main), "Animal");
			}

			ModelState.AddModelError("Login", "Invalid email or password");

			return View(loginDTO);
		}

		[HttpGet]
		[Route("/[action]")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction(nameof(AnimalController.Main), "Animal");
		}

		public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
		{
			ApplicationUser user = await _userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return Json(true);
			}

			return Json(false);
		} 
	}
}
