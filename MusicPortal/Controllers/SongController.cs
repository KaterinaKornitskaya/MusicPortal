using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class SongController : Controller
    {
        readonly IMusicRepository<Song> _repository;
        readonly IUserRepository _userRepository;
        public SongController(IMusicRepository<Song> repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository=userRepository;
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

        public IActionResult AllSongs()
        {
            IsAdmin();
            return View(_repository.GetAll());
        }

        public IActionResult MySongs()
        {
            var mySongsList = _repository.GetAll().Where(e => e.User.Email == HttpContext.Session.GetString("Email"));
            IsAdmin();
            return View(mySongsList);
        }

        public IActionResult Create()
        {
            
            ViewData["GenreId"] = new SelectList(_repository.GenreList(),"Id", "Title");
            //ViewBag.GenreId = new SelectList(_repository.GenreList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Song song, IFormFile? uploadedFilePath, IFormFile? uploadedFileImage)
        {
            try
            {
                string email = HttpContext.Session.GetString("Email");
                var user = _repository.GetUserByEmail(email);
                song.User = user;
                await _repository.Create(song, uploadedFilePath, uploadedFileImage);
                return RedirectToAction(nameof(AllSongs));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AllSongs));
            }

        }

        //GET: Account/Delete/Id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var song = await _repository.GetById(id);
            if (song == null)
                return NotFound();

            return View(song);
        }

        // POST: Account/Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteById(id); 
            return RedirectToAction(nameof(AllSongs));
        }

       
    }
}
