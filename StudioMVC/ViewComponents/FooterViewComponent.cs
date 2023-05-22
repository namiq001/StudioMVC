using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioMVC.Models;
using StudioMVC.StudioDataContext;

namespace StudioMVC.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    public readonly StudioDbContext _context;

    public FooterViewComponent(StudioDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        Dictionary<string, Setting> settings = await _context.Settings.ToDictionaryAsync(s => s.Key);
        return View(settings);
    }
}
