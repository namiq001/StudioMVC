using StudioMVC.Models;

namespace StudioMVC.ViewModels.WorkerVM;

public class CreateWorkerVM
{
    public string Name { get; set; } = null!;
    public int WorkId { get; set; }
    public IFormFile Image { get; set; } = null!;
    public List<Work>? Works { get; set; }

}
