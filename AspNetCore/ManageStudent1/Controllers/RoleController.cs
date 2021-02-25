using ManageStudent1.Models;
using ManageStudent1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                                        UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // actions

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(Role model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };

                IdentityResult result = await this.roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = this.roleManager.Roles;
            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if(id is null)
            {
                return View("../Errors/NotFound", "Please add the role Id in URL");
            }
            IdentityRole role = await this.roleManager.FindByIdAsync(id);
            if(role is null)
            {
                return View("../Errors/NotFound", $"The role Id : {id} cannot be found");
            }

            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach (AppUser user in await userManager.Users.ToListAsync())
            {
                if( await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Email);
                }
            } 
            return View(model);
        }


        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id is null)
            {
                return View("../Errors/NotFound", "Please add the role Id in URL");
            }
            IdentityRole role = await this.roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
            }

            return RedirectToAction("ListRoles");


        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await this.roleManager.FindByIdAsync(model.Id);
                if (role is null)
                {
                    return View("../Errors/NotFound", $"The role Id : {model.Id} cannot be found");
                }
                role.Name = model.RoleName;

                IdentityResult result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }











    }
}
