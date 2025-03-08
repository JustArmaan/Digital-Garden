using Microsoft.AspNetCore.Mvc;
using MVCView.Models;
using MVCView.Repositories;
using System.Threading.Tasks;

namespace MVCView.Controllers
{
    public class CommunityTipController : Controller
    {
        private readonly ICommunityTipRepository _communityTipRepository;

        public CommunityTipController(ICommunityTipRepository communityTipRepository)
        {
            _communityTipRepository = communityTipRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tips = await _communityTipRepository.GetTips();
            return View(tips);
        }

        public async Task<IActionResult> Details(int id)
        {
            var tip = await _communityTipRepository.GetTip(id);
            if (tip == null)
            {
                return NotFound();
            }
            return View(tip);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Content,SubmittedBy")] CommunityTip tip)
        {
            if (ModelState.IsValid)
            {
                tip.SubmittedDate = DateTime.Now;
                await _communityTipRepository.AddTip(tip);
                return RedirectToAction(nameof(Index));
            }
            return View(tip);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tip = await _communityTipRepository.GetTip(id);
            if (tip == null)
            {
                return NotFound();
            }
            return View(tip);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,SubmittedBy,SubmittedDate")] CommunityTip tip)
        {
            if (id != tip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _communityTipRepository.UpdateTip(tip);
                return RedirectToAction(nameof(Index));
            }
            return View(tip);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tip = await _communityTipRepository.GetTip(id);
            if (tip == null)
            {
                return NotFound();
            }
            return View(tip);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _communityTipRepository.DeleteTip(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
