using Microsoft.EntityFrameworkCore;
using WeatherApp.Models;

namespace WeatherApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; } = null!;
    public DbSet<TodoList> TodoLists { get; set; } = null!;

    public static string DbPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WeatherApp.db");

    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
