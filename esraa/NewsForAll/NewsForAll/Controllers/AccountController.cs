using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsForAll.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsForAll.Controllers
{
    [Authorize(Roles ="Admin1")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new IdentityUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.Phone };
                var r = await userManager.CreateAsync(User, model.Password);
                if (r.Succeeded)
                {
                    await signInManager.SignInAsync(User, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var err in r.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }

            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (user.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Email/Password");
            }
            return View(model);


        }
        [HttpGet]
    
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Account");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }

            }
            return View(model);
        }
       
        public IActionResult RoleList()
        {
            var result = roleManager.Roles;
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new EditRoleViewModel { Id = role.Id, RoleName = role.Name };
            foreach (var user in userManager.Users)
            {
                if ( await userManager.IsInRoleAsync(user,model.RoleName))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Account");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            HttpContext.Session.SetString("RoleId", id);
            var r = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if ( await userManager.IsInRoleAsync(user,role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                r.Add(userRole);
               
            }
            return View(r);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole (List<UserRoleViewModel> model)
        {
            string id = HttpContext.Session.GetString("RoleId");
            var role =await roleManager.FindByIdAsync(id);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult r = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name))) { 

                r = await userManager.AddToRoleAsync(user, role.Name);
                } else if(!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name)){
                    r = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("RoleList");
        }
        
    }
}
