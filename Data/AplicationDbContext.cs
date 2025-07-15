using Microsoft.EntityFrameworkCore;
using MyperPrueba.Models;
using System.Collections.Generic;

namespace MyperPrueba.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Distrito> Distritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.ToTable("Trabajadores");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                
                entity.Property(e => e.TipoDocumento).HasMaxLength(3);
                entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
                entity.Property(e => e.Nombres).HasMaxLength(500);
                entity.Property(e => e.Sexo).HasMaxLength(1);
                

                
                entity.HasOne(t => t.Departamento)
                      .WithMany()
                      .HasForeignKey(t => t.IdDepartamento)
                      .HasPrincipalKey(d => d.Id)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(t => t.Provincia)
                      .WithMany()
                      .HasForeignKey(t => t.IdProvincia)
                      .HasPrincipalKey(p => p.Id)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Distrito)
                      .WithMany()
                      .HasForeignKey(t => t.IdDistrito)
                      .HasPrincipalKey(di => di.Id)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.Restrict);
            });

            
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.NombreDepartamento).HasMaxLength(500);
            });

            
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("Provincia");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.NombreProvincia).HasMaxLength(500);
                

                entity.HasOne(p => p.Departamento)
                      .WithMany(d => d.Provincias)
                      .HasForeignKey(p => p.IdDepartamento)
                      .HasPrincipalKey(d => d.Id)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.Restrict);
            });

            
            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.ToTable("Distrito");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.NombreDistrito).HasMaxLength(500);
                

                entity.HasOne(d => d.Provincia)
                      .WithMany(p => p.Distritos)
                      .HasForeignKey(d => d.IdProvincia)
                      .HasPrincipalKey(p => p.Id)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
