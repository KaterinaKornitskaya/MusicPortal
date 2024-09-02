using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;

        IMusicRepository<Song> _musicRepository;
        //private readonly MPContext _context;

      
        public AccountController(IUserRepository userRepository, 
            IMusicRepository<Song> musicRepository)
        {
            _userRepository = userRepository;
            _musicRepository=musicRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Account
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel regModel)
        {
            User user = new User();
            await _userRepository.CreateUser(user, regModel);
            await _userRepository.AddUserToDB(user);
            return RedirectToAction(nameof(Authorization));
        }

        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authorization(AuthorizationModel authModel)
        {
            if (_userRepository.LogIn(authModel) == true)
            {
                var musList = _musicRepository.GetAll();

                HttpContext.Session.SetString("Email", authModel.Email ?? string.Empty);
                var user = _userRepository.GetAllUsers().FirstOrDefault(e => e.Email == authModel.Email);

                HttpContext.Session.SetString("Name", user.Name ?? string.Empty);
               
                if (/*(user != null && user.IsAdmin == true) ||*/ (user!=null && user.IsConfirmed == true))
                {
                    if(user.IsAdmin == true)
                    {
                        ViewBag.IsAdmin = true;
                        return View("~/Views/Song/AllSongs.cshtml", musList);
                        //return View("AllSongs", musList);
                        //return View(nameof(Index), musList);
                    }
                    else
                    {
                        ViewBag.IsAdmin = false;
                        return View("~/Views/Song/AllSongs.cshtml", musList);
                    }                                     
                }  
                
                else
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
            }
            else
                return View();
        }
    }
}
