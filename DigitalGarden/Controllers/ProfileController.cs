using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DigitalGarden.Models;
using MVCView.Models;

namespace MVCView.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IProfileRepository profileRepository, UserManager<ApplicationUser> userManager)
        {
            _profileRepository = profileRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return NotFound();

            var user = await _profileRepository.GetProfileByUserId(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var profiles = await _profileRepository.GetAllProfiles();
            return View(profiles);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var profile = await _profileRepository.GetProfileById(id);
            if (profile == null) return NotFound();

            return View(profile);
        }

        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return NotFound();

            var user = await _profileRepository.GetProfileByUserId(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser model)
        {
            if (!ModelState.IsValid) return View(model);

            var currentUserId = _userManager.GetUserId(User);
            if (currentUserId != model.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            await _profileRepository.UpdateProfile(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var profile = await _profileRepository.GetProfileById(id);
            if (profile == null) return NotFound();

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _profileRepository.DeleteProfile(id);
            return RedirectToAction(nameof(List));
        }
    }
}
