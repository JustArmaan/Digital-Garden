using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCView.Models;

namespace MVCView.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var profile = await _profileRepository.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _profileRepository.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost]
        public IActionResult Edit(Profile profile)
        {
            if (ModelState.IsValid)
            {
                _profileRepository.UpdateProfile(profile);
                return RedirectToAction(nameof(Index), new { id = profile.Id });
            }
            return View(profile);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _profileRepository.GetProfile(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _profileRepository.DeleteProfile(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
