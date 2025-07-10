using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Destiny> Destinies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Exemplo de configuração de relacionamento (opcional)
        modelBuilder
            .Entity<Client>()
            .HasMany(c => c.Destinies)
            .WithOne()
            .HasForeignKey(d => d.ClientId);
        modelBuilder
            .Entity<Client>()
            .HasMany(c => c.Documents)
            .WithOne()
            .HasForeignKey(d => d.ClientId);
    }
}
