using DigitalDevices.AuthApp;
using DigitalDevices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DigitalDevices.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountsController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid){
            var loginResult = await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                isPersistent: false,
                false);
                return RedirectToAction("Index", "Home");
            }
                ModelState.AddModelError("", "Пользователь не найден!");
                return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username
                };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    var config = _configuration.GetSection("SecretCodes");
                    if (!String.IsNullOrEmpty(model.SecretCode)) 
                    {
                        if(model.SecretCode == config["AdminCode"])
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        if (model.SecretCode == config["ManagerCode"])
                        {
                            await _userManager.AddToRoleAsync(user, "Manager");
                        }
                    }
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Manage()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var users = _userManager.Users
                .Where(u => u.Id != currentUser.Id);
            var userRoles = new List<UserRolesViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add( new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = roles.ToList(),
                    IsAdmin = roles.Contains("Admin")
                });
            }
            var model = new PaginatedList<UserRolesViewModel>(userRoles,userRoles.Count,1,10);
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateRoles(string userId, List<string> roles)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var targetUser = await _userManager.FindByIdAsync(userId);

            if (await _userManager.IsInRoleAsync(targetUser, "Admin"))
            {
                return Forbid();
            }

            var currentRoles = await _userManager.GetRolesAsync(targetUser);
            await _userManager.RemoveFromRolesAsync(targetUser, currentRoles);

            roles.Add("User");
            await _userManager.AddToRolesAsync(targetUser, roles.Distinct());

            return RedirectToAction(nameof(Manage));
        }
    }
}
