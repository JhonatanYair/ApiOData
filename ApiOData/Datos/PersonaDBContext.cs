
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

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PersonaDB;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");
//            }
//        }

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
