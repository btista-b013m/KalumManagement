using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace KalumManagement
{
    public class KalumDBContext : DbContext
    {
        public KalumDBContext(DbContextOptions options) 
            : base(options)
        {
        }
        public DbSet<CarreraTecnica> CarrerasTecnicas {get; set;}
        public DbSet<Jornada> Jornadas {get; set;}
        public DbSet<ExamenAdmision> ExamenAdmisiones {get; set;}
        public DbSet<Aspirante> Aspirante {get;set;}
        public DbSet<InscripcionPago> InscripcionPago {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("carreratecnica").HasKey(ct => new {ct.CarreraId});
            modelBuilder.Entity<Jornada>().ToTable("jornada").HasKey(ct => new {ct.JornadaId});
            modelBuilder.Entity<ExamenAdmision>().ToTable("examenadmision").HasKey(ct => new{ct.examenid});
            modelBuilder.Entity<Aspirante>().ToTable("aspirante").HasKey(a => new {a.NoExpediente});
            modelBuilder.Entity<InscripcionPago>().ToTable("inscripcionpago").HasKey(ip => new {ip.BoletaPago, ip.Anio, ip.NoExpediente});
            modelBuilder.Entity<InscripcionPago>().Property(ip => ip.FechaPago).HasColumnType("datetime2");

           modelBuilder.Entity<Aspirante>()
                .HasOne<CarreraTecnica>( a => a.CarreraTecnica)
                .WithMany(ct => ct.Aspirantes)
                .HasForeignKey(a => a.CarreraId);

            modelBuilder.Entity<Aspirante>()
                .HasOne<Jornada>( a => a.Jornada)
                .WithMany(j => j.Aspirantes)
                .HasForeignKey(a => a.JornadaId);

            modelBuilder.Entity<Aspirante>()
                .HasOne<ExamenAdmision>(a => a.ExamenAdmision)
                .WithMany(ea => ea.Aspirantes)
                .HasForeignKey(a => a.ExamenId);

        }


    }
}