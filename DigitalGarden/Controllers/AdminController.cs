using DigitalGarden.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCView.Models;
using MVCView.Repositories;

namespace DigitalGarden.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICommunityTipRepository _communityTipRepository;

        public AdminController(
            IProfileRepository profileRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ICommunityTipRepository communityTipRepository)
        {
            _profileRepository = profileRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _communityTipRepository = communityTipRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _profileRepository.GetAllProfiles();
            return View(users);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _profileRepository.GetProfileById(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = roles;

            return View(user);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _profileRepository.GetProfileById(id);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            ViewBag.UserRoles = userRoles;
            ViewBag.AllRoles = _roleManager.Roles.ToList();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, ApplicationUser model, List<string> selectedRoles)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                        return NotFound();

                    // Update user properties
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.IsAdmin = selectedRoles.Contains("Admin");
                    user.GardeningExperience = model.GardeningExperience;
                    user.City = model.City;
                    user.Gender = model.Gender;

                    await _userManager.UpdateAsync(user);

                    // Update roles
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        if (!selectedRoles.Contains(role))
                            await _userManager.RemoveFromRoleAsync(user, role);
                    }
                    foreach (var role in selectedRoles)
                    {
                        if (!userRoles.Contains(role))
                            await _userManager.AddToRoleAsync(user, role);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
                }
            }

            var currentRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(id));
            ViewBag.UserRoles = currentRoles;
            ViewBag.AllRoles = _roleManager.Roles.ToList();

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _profileRepository.GetProfileById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            await _profileRepository.DeleteProfile(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CommunityTips()
        {
            var tips = await _communityTipRepository.GetAllCommunityTips();
            return View(tips);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommunityTip(int id)
        {
            await _communityTipRepository.DeleteCommunityTip(id);
            return RedirectToAction(nameof(CommunityTips));
        }
    }
}
