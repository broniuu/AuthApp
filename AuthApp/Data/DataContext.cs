using AuthApp.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Data;

public class DataContext : DbContext
{
    public DbSet<AppUser> AppUsers { get; set; }
    public string DbPath { get; }
    
    public DataContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "data.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}