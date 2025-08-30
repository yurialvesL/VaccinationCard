using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Context;

/// <summary>
/// Represents the database context for the application, providing access to the entities and configuring their
/// relationships and constraints.
/// </summary>
/// <remarks>This class is derived from <see cref="DbContext"/> and is used to interact with the database. It
/// includes <see cref="DbSet{TEntity}"/> properties for the application's main entities: <see cref="Person"/>, <see
/// cref="Vaccine"/>, and <see cref="Vaccination"/>. The context also configures entity relationships, constraints, and
/// default values using the <see cref="OnModelCreating(ModelBuilder)"/> method.  
/// Use this class to query and save instances of the application's entities. Typically, it is configured with dependency injection and a connection
/// string in the application's startup.</remarks>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasIndex(p => p.Id).IsUnique();
            entity.Property(x => x.UpdateAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(p => new { p.Id, p.CPF }).IsUnique();
            entity.Property(x => x.UpdateAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasOne(v => v.Person)
                  .WithMany(p => p.Vaccinations)
                  .HasForeignKey(v => v.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(v => v.Vaccine)
                  .WithMany(x => x.Vaccinations)
                  .HasForeignKey(v => v.VaccineId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(v => new { v.PersonId, v.VaccineId, v.Dose }).IsUnique();
        });
    }
}
