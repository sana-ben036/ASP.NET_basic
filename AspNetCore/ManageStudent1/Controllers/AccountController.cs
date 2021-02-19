using Microsoft.AspNetCore.Mvc;
using ManageStudent1.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ManageStudent1.Models.ViewModels;

namespace ManageStudent1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }


        //Actions

        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(Account model )
        {
            if (ModelState.IsValid)
            {
                string FullName = GeneratUserName(model.FirstName, model.LastName);
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email ,
                    Email = model.Email
                };

                 var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("List", "Student");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


                
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Student");
                }
                ModelState.AddModelError("", "Login Invalid.");


            }
            return View(model);
        }





        public string GeneratUserName(string FirstName , string LastName)
        {
            return FirstName.Trim().ToUpper() + "_" + LastName.Trim().ToLower();
        }










    }
}
