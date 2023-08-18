
using ApiOData.Datos.Configurations;
using ApiOData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
namespace ApiOData.Datos
{
    public partial class PersonaDBContext : DbContext
    {
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Hijo> Hijo { get; set; }
        public virtual DbSet<Padre> Padre { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }

        public PersonaDBContext()
        {
        }

        public PersonaDBContext(DbContextOptions<PersonaDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.GeneroConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.HijoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PadreConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PersonaConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
