using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.Repository;
using NuGet.Protocol.Core.Types;
using System.Net;

namespace MusicPortal.Controllers
{
    public class UserController : Controller
    {
        readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
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

        public IActionResult AllUsers()
        {
            var userList = _userRepository.GetAllUsers();
            IsAdmin();
            return View(userList);
        }

        public IActionResult Details(int id)
        {
            var user = _userRepository.GetUserById(id);
            ViewBag.SongList = user.Songs;
            IsAdmin();
            return View(user);
        }

        public async Task<IActionResult> ConfirmUser(int id)
        {
            await _userRepository.ConfirmUser(id);
            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var user = _userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _userRepository.DeleteById(id);
                return RedirectToAction(nameof(AllUsers));
            }
            catch
            {
                return RedirectToAction(nameof(AllUsers));
            }

        }
    }
}
