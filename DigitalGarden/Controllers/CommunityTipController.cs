using Microsoft.AspNetCore.Mvc;
using MVCView.Models;

namespace MVCView.Controllers
{
    public class CommunityTipController : Controller
    {
        private readonly ICommunityTipRepository _communityTipRepository;

        public CommunityTipController(ICommunityTipRepository communityTipRepository)
        {
            _communityTipRepository = communityTipRepository;
        }

        public IActionResult Index()
        {
            var tips = _communityTipRepository.GetTips();
            return View(tips);
        }

        public IActionResult Details(int id)
        {
            var tip = _communityTipRepository.GetTip(id);
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
        public IActionResult Create([Bind("Title,Content,SubmittedBy")] CommunityTip tip)
        {
            if (ModelState.IsValid)
            {
                tip.SubmittedDate = DateTime.Now;
                _communityTipRepository.AddTip(tip);
                return RedirectToAction(nameof(Index));
            }
            return View(tip);
        }

        public IActionResult Edit(int id)
        {
            var tip = _communityTipRepository.GetTip(id);
            if (tip == null)
            {
                return NotFound();
            }
            return View(tip);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Title,Content,SubmittedBy,SubmittedDate")] CommunityTip tip)
        {
            if (id != tip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _communityTipRepository.UpdateTip(tip);
                return RedirectToAction(nameof(Index));
            }
            return View(tip);
        }

        public IActionResult Delete(int id)
        {
            var tip = _communityTipRepository.GetTip(id);
            if (tip == null)
            {
                return NotFound();
            }
            return View(tip);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _communityTipRepository.DeleteTip(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
