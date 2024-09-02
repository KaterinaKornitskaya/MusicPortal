using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.Repository;
using MusicPortal.Services;
using System.Diagnostics;

namespace MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMusicRepository<Song> _musicRepository;

        public HomeController(ILogger<HomeController> logger, IMusicRepository<Song> musicRepository)
        {
            _logger = logger;
            _musicRepository = musicRepository;
        }

        public IActionResult Index()
        {
            // ��� �������������� ������������� ���������� ������ 10 �����
            var musList = _musicRepository.GetAll().Take(9).ToList();
            return View(musList);
        }

        // ����� �� ��������
        public ActionResult Logout()
        {
            // ��������� ������
            HttpContext.Session.Clear();
            // ������������� �� Index �� ����������� Home
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
