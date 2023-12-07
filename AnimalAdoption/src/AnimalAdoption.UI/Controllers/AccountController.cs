using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Core.DTO.Identity;
using AnimalAdoption.Core.Enums;
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
		private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(
				UserManager<ApplicationUser> userManager,
				SignInManager<ApplicationUser> signInManager,
				RoleManager<ApplicationRole> roleManager
			)
        {
            _userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager; 
        }

        [HttpGet]
		[Route("/[action]")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[Route("/[action]")]
		[ValidateAntiForgeryToken]
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
				if (registerDTO.UserType is UserTypeOptions.Admin)
				{
					if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
					{
						ApplicationRole adminRole = new ApplicationRole()
						{
							Name = UserTypeOptions.Admin.ToString()
						};

						await _roleManager.CreateAsync(adminRole);
					}

					await _userManager.AddToRoleAsync(user, nameof(UserTypeOptions.Admin));
				}
				else
				{
					if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
					{
						ApplicationRole userRole = new ApplicationRole()
						{
							Name = UserTypeOptions.User.ToString()
						};

						await _roleManager.CreateAsync(userRole);
					}

					await _userManager.AddToRoleAsync(user, nameof(UserTypeOptions.User));
				}

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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
		{
			if (ModelState.IsValid == false)
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
			ViewBag.LoginError = "Invalid email or password";

			return View(loginDTO);
		}

		[HttpGet]
		[Route("/[action]")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction(nameof(AnimalController.Main), "Animal");
		}

		[HttpGet]
		[Route("[controller]/[action]")]
		public async Task<IActionResult> AccessDenied()
		{
			return View();
		}

		[Route("/[action]")]
		public async Task<IActionResult> IsEmailAlreadyRegistered(string Email)
		{
			if (string.IsNullOrEmpty(Email))
			{
				return Json(false);
			}

			ApplicationUser user = await _userManager.FindByEmailAsync(Email);

			if (user == null)
			{
				return Json(true);
			}

			return Json(false);
		} 
	}
}
