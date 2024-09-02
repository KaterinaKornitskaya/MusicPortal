using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class GenreController : Controller
    {
        IMusicRepository<Genre> _genreRepository;
        IUserRepository _userRepository;
        public GenreController(IMusicRepository<Genre> genreRepository, IUserRepository userRepository)
        {
            _genreRepository = genreRepository;
            _userRepository = userRepository;
        }

        private void IsAdmin()
        {
            string email = HttpContext.Session.GetString("Email");
            var user = _userRepository.GetUserByEmail(email);
            if (user.IsAdmin == true)
                ViewBag.IsAdmin = true;
            else
                ViewBag.IsAdmin = false;
        }

        public IActionResult AllGenres()
        {
            IsAdmin();
            return View(_genreRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre, IFormFile? uploadedFile, IFormFile? uploadedFile2)
        {
            try
            {
                await _genreRepository.Create(genre, uploadedFile, uploadedFile2);
                return RedirectToAction(nameof(AllGenres));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllGenres));
            }
        }      

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var x = await _genreRepository.GetById(id);
           
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
                await _genreRepository.DeleteById(id);
                return RedirectToAction(nameof(AllGenres));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllGenres));
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
                return NotFound();
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
                return NotFound();
            return View(genre);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditThis([Bind] Genre genre)
        {
            try
            {
                await _genreRepository.UpdateById(genre);
                return RedirectToAction(nameof(AllGenres));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllGenres));
            }
        }
    }
}
