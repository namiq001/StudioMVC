namespace StudioMVC.Models;

public class Work
{
    public int Id { get; set; }
    public string WorkName { get; set; } = null!;
    public List<Worker> Workers { get; set; }
}
