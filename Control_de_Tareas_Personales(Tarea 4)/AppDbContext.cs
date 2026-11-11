using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private string connection = @"Server=localhost;Database=ControlTareasDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>()
            .Property(t => t.Status)
            .HasConversion<string>();
    }
}