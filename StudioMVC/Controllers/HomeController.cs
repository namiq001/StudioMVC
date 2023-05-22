using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioMVC.Models;
using StudioMVC.StudioDataContext;
using StudioMVC.ViewModels;

namespace StudioMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudioDbContext _context;

        public HomeController(StudioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Worker> worker = await _context.Workers.Include(x => x.Works).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Workers = worker,
            };
            return View(homeVM);
        }
    }
}