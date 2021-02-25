using Microsoft.AspNetCore.Mvc;
using ManageStudent1.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ManageStudent1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace ManageStudent1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }


        //Actions


        [AllowAnonymous]
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> CheckingExistingEmail(Account model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"This Email {model.Email} is already in use!");
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Account model )
        {
            if (ModelState.IsValid)
            {

                AppUser user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age= model.Age,
                    UserName = model.Email,
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
            return RedirectToAction("index", "Student");
        }




        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("List", "Student");
                    }
                    
                }
                ModelState.AddModelError(string.Empty, "Login Invalid!!");


            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditAccount(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                AppUser user = await userManager.FindByIdAsync(id);
                if(user!= null)
                {
                    EditAccountViewModel model = new EditAccountViewModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Id = user.Id,
                        Password = user.PasswordHash,
                        ConfirmPassword = user.PasswordHash
                    };
                    return View(model);
                }
            }
            return RedirectToAction("index", "Student");   
        }


        [HttpPost]
        public async Task<IActionResult> EditAccount(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Age = model.Age;

                    var PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
                    user.PasswordHash = PasswordHash;


                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Student");
                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }
            return View(model);
        }


        



        // external login

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            AccountLoginViewModel model = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);

        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            AccountLoginViewModel model = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if(remoteError != null)
            {
                ModelState.AddModelError("", $"Error from external provider : {remoteError}");
                return View("Login",model);
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", $"Error loading external login information");
                return View("Login", model);
            }
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                     info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if(email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if(user == null)
                    {
                        user = new AppUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)

                        };

                        await userManager.CreateAsync(user);
                    }

                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from : {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on sana.bengannoune@gmail.com";

                return View("Error");
            }

            



        }














    }
}
