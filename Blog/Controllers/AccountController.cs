using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace TechBlog.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //Validation errors
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(err => err.ErrorMessage).ToList();
                return View();
            }

            //Create new user
            ApplicationUser user = new() { FirstName = registerDTO.FirstName, LastName = registerDTO.LastName, 
             Email = registerDTO.Email, PhoneNumber = registerDTO.Phone, UserName = registerDTO.Email };
            
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password!);

            //Failed user
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(registerDTO);
            }

            if (registerDTO.UserType == UserTypeOptions.Admin)
            {
                //Create Admin Role if not exist
                if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                {
                    ApplicationRole role = new() { Name = UserTypeOptions.Admin.ToString() };
                    await _roleManager.CreateAsync(role);
                }

                //Add user to role
                await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
            }
            else
            {
                //Create Author Role if not exist
                if (await _roleManager.FindByNameAsync(UserTypeOptions.Author.ToString()) is null)
                {
                    ApplicationRole role = new() { Name = UserTypeOptions.Author.ToString() };
                    await _roleManager.CreateAsync(role);
                }
                //Role already Exist
                await _userManager.AddToRoleAsync(user, UserTypeOptions.Author.ToString());
            }

            //Sign in registered user
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction(nameof(AdminController.Index), "Admin");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {

            //Validation errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(err => err.ErrorMessage).ToList();
                return View();
            }

            var result =  await _signInManager.PasswordSignInAsync(loginDTO.Email!, loginDTO.Password!, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //Admin
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email!);

                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }

                if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }

                //return RedirectToAction(nameof(PersonsController.Index), "Persons");
            }

            ModelState.AddModelError("Login", "Invalid email or password");
            return View(loginDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> IsEmailExist(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
