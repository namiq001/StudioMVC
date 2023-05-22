using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioMVC.Models;
using StudioMVC.StudioDataContext;

namespace StudioMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class SettingController : Controller
{
    private readonly StudioDbContext _context;

    public SettingController(StudioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Setting> settings = await _context.Settings.ToListAsync();
        return View(settings);
    }
}
