using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioMVC.Models;

namespace StudioMVC.StudioDataContext;

public class StudioDbContext : IdentityDbContext<AppUser>
{

	public StudioDbContext(DbContextOptions<StudioDbContext> options) : base(options)
	{

	}
	public DbSet<Work> Works { get; set; }
	public DbSet<Worker> Workers { get; set; }
	public DbSet<Setting> Settings { get; set; }
}
