namespace StudioMVC.Models;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public int WorkId { get; set; }
    public Work Works { get; set; } = null!;

}
