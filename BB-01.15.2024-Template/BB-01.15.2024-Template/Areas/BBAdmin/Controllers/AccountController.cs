using BB_01._15._2024_Template.Areas.BBAdmin.ViewModels;
using BB_01._15._2024_Template.Models;
using BB_01._15._2024_Template.Utilities.Enums;
using BB_01._15._2024_Template.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BB_01._15._2024_Template.Areas.BBAdmin.Controllers
{
    [Area("BBAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (userVM.IssSymbol(userVM.UserName))
            {
                ModelState.AddModelError("UserName", "you Cannot include symbol");
                return View();
            }
            if (userVM.IssDigit(userVM.UserName))
            {
                ModelState.AddModelError("UserName", "you Cannot include digit");
                return View();
            }
            AppUser user = new AppUser
            {
                UserName = userVM.UserName,
                Country = userVM.Country,
                Email = userVM.Email
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, userVM.Password);    
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                    return View();  
                }
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index","Home"); 
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home"); 
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser existedUser = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if (existedUser is null)
            {
                existedUser = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (existedUser is null)
                {
                    ModelState.AddModelError(String.Empty, "Username or Email is incorrect!");
                    return View();
                }
            }
            var passwordResult = await _signInManager.PasswordSignInAsync(existedUser, loginVM.Password, loginVM.IsRemembered, true);
            if (passwordResult.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "You Blocked haha");
                return View();
            }
            if (!passwordResult.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username or Email is Incorrect!Please try another");
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> CreateRoles()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role.ToString(),
                });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
