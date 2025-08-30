using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Context;

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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vaccine>()
            .HasOne(i => i.Person)
            .WithMany(v => v.Vaccines)
            .HasForeignKey(i => i.Id);

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.Property(x => x.UpdateAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<Person>()
            .HasIndex(p => p.CPF)
            .IsUnique();


        modelBuilder.Entity<Person>()
            .HasMany(u => u.Vaccines)
            .WithOne(i => i.Person)
            .HasForeignKey(i => i.Id);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(x => x.UpdateAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        });
    }

 
}
