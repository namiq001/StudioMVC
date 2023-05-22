using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioMVC.Models;
using StudioMVC.StudioDataContext;
using StudioMVC.ViewModels.WorkerVM;

namespace StudioMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class WorkerController : Controller
{
    private readonly StudioDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public WorkerController(StudioDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Worker> workers = await _context.Workers.Include(x => x.Works).ToListAsync();
        return View(workers);
    }
    public async Task<IActionResult> Create()
    {
        CreateWorkerVM createWorker = new CreateWorkerVM()
        {
            Works=await _context.Works.ToListAsync(),
        };
        return View(createWorker);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateWorkerVM createWorker)
    { 
        createWorker.Works = await _context.Works.ToListAsync();
        if (!ModelState.IsValid) { return View(createWorker); }
        string newFileName = Guid.NewGuid().ToString() + createWorker.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath,"assets","img", newFileName);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await createWorker.Image.CopyToAsync(fileStream);
        }
        Worker worker = new Worker()
        {
            Name = createWorker.Name,
            WorkId = createWorker.WorkId,
        };
        worker.ImageName = newFileName;

        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int Id)
    { 
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null) { return NotFound(); }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", worker.ImageName);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null)
            { return NotFound(); }
        EditWorkerVM editWorker = new EditWorkerVM()
        {
            Name = worker.Name,
            WorkId = worker.WorkId,
            Works = await _context.Works.ToListAsync(),
            ImageName = worker.ImageName,
        };
        return View(editWorker);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditWorkerVM editWorker)
    {
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null)
        { return NotFound(); }
        if (!ModelState.IsValid)
        { 
            editWorker.Works = await _context.Works.ToListAsync();
            return View(editWorker);
        }
        if (editWorker.ImageName is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", worker.ImageName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editWorker.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", newFileName);
            using (FileStream fileStream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editWorker.Image.CopyToAsync(fileStream);
            }
            worker.ImageName = newFileName;
        }
        worker.Name = editWorker.Name;
        worker.WorkId = editWorker.WorkId;
        _context.Workers.Update(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
