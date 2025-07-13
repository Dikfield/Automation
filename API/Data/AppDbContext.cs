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
            .Entity<Destiny>()
            .HasOne(d => d.Client)
            .WithMany(c => c.Destinies)
            .HasForeignKey(d => d.ClientId);

        modelBuilder
            .Entity<Document>()
            .HasOne(d => d.Client)
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.ClientId);
    }
}
