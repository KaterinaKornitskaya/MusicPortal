using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class SongController : Controller
    {
        readonly IMusicRepository<Song> _repository;
        public SongController(IMusicRepository<Song> repository)
        {
            _repository = repository;
        }

        public IActionResult AllSongs()
        {
            return View(_repository.GetAll());
        }

        public IActionResult Create()
        {
            
            ViewData["GenreId"] = new SelectList(_repository.GenreList(),"Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Song song)
        {
            try
            {
                await _repository.Create(song);
                return RedirectToAction(nameof(AllSongs));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllSongs));
            }

        }
    }
}
