using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class GenreController : Controller
    {
        IMusicRepository<Genre> _repository;
        public GenreController(IMusicRepository<Genre> repository)
        {
            _repository=repository;
        }

        public IActionResult AllGenres()
        {
            //return _repository.GetAll() != null 
            //    ? View(  _repository.GetAll()) 
            //    : Problem("Entity set 'SoccerContext.Teams'  is null.");
           
            return View(_repository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            try
            {
                await _repository.Create(genre);
                return View(genre);
               // return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllGenres));
            }
        }

        public IActionResult Delete()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var x = await _repository.GetById(id);
            //Films
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (x == null)
            {
                return NotFound();
            }

            return View(x);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
            try
            {
                await _repository.DeleteById(id);
                return RedirectToAction(nameof(AllGenres));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllGenres));
            }
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {
            try
            {
                await _repository.Update(genre);
                return RedirectToAction(nameof(AllGenres));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllGenres));
            }
        }
    }
}
