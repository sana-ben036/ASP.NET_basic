using ImTools;
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
                RoleId = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach (AppUser user in await userManager.Users.ToListAsync()) // userManager.Users is not awaitble so change to (await userManager.Users.ToListAsync())
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
                var role = await this.roleManager.FindByIdAsync(model.RoleId);
                if (role is null)
                {
                    return View("../Errors/NotFound", $"The role Id : {model.RoleId} cannot be found");
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


        [HttpGet]
        public async Task<IActionResult> EditUsersRole(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View("../Errors/NotFound", $"The role must be exist and not empty in Url");

            }
            var role = await this.roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return View("../Errors/NotFound", $"The role Id : {role.Id} cannot be found");
            }

            var Models = new List<EditUsersRoleViewModel>();
            foreach (var user in await userManager.Users.ToListAsync())
            {
                EditUsersRoleViewModel model = new EditUsersRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    //IsSelected = false
                };
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.IsSelected = true;
                }
                else
                {
                    model.IsSelected = false;
                }

                Models.Add(model);
            }
            ViewBag.RoleId = roleId;
            return View(Models);





        }



        [HttpPost]
        public async Task<IActionResult> EditUsersRole(List<EditUsersRoleViewModel> model , string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View("../Errors/NotFound", $"The role must be exist and not empty in Url");

            }
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return View("../Errors/NotFound", $"The role Id : {role.Id} cannot be found");
            }

            // role if deja affectté et in model is select il faut le supprimer , ou l'affecté si il est selecté au model mais non affecté before


            IdentityResult result = null;
            for (int i = 0; i < model.Count; i++) 
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)) )
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user,role.Name)) )
                {
                     result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                
                else
                {
                    continue;
                }

               

           

            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


                //if (i < (model.Count - 1))
                //    continue;
                //else
                //    return RedirectToAction("EditRole", new { Id = roleId });
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }


    }
}
