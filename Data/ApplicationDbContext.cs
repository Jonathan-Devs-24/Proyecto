using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Models.Entidades;

namespace SaludTotalAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Entidades
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Informe> Informes { get; set; }
        public DbSet<Profesional_Especialidad> Profesional_Especialidades { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }

        // OnModelCreating para configurar las relaciones y restricciones de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FORZAR nombres de tablas... por si acaso
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Paciente>().ToTable("Paciente");
            modelBuilder.Entity<Profesional>().ToTable("Profesional");
            modelBuilder.Entity<Especialidad>().ToTable("Especialidad");
            modelBuilder.Entity<Profesional_Especialidad>().ToTable("Profesional_Especialidad");
            modelBuilder.Entity<Disponibilidad>().ToTable("Disponibilidad");
            modelBuilder.Entity<Turno>().ToTable("Turno");
            modelBuilder.Entity<Informe>().ToTable("Informe");

            // Configuración que permite relaciones entre profesionales y especialidades Uno a Muchos
            modelBuilder.Entity<Profesional_Especialidad>()
                        .HasKey(pe => new { pe.Id_Profesional, pe.Id_Especialidad });

            modelBuilder.Entity<Profesional_Especialidad>()
                        .HasOne(pe => pe.Profesional)
                        .WithMany()
                        .HasForeignKey(pe => pe.Id_Profesional)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Profesional_Especialidad>()
                        .HasOne(pe => pe.Especialidad)
                        .WithMany()
                        .HasForeignKey(pe => pe.Id_Especialidad)
                        .OnDelete(DeleteBehavior.Restrict);

            // Configuración que permite relaciones entre pacientes y turnos Uno a Muchos
            modelBuilder.Entity<Turno>()
                        .HasOne(t => t.Paciente)
                        .WithMany()
                        .HasForeignKey(t => t.Id_Paciente)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turno>()
                        .HasOne(t => t.Profesional)
                        .WithMany()
                        .HasForeignKey(t => t.Id_Profesional)
                        .OnDelete(DeleteBehavior.Restrict);

            // Configuración que permite relaciones entre informes y turnos Uno a Muchos
            modelBuilder.Entity<Informe>()
                        .HasOne(i => i.Turno)
                        .WithMany()
                        .HasForeignKey(i => i.Id_Turno)
                        .OnDelete(DeleteBehavior.Restrict);

            // Configuración que permite relaciones entre Disponibilidades y profesionales
            modelBuilder.Entity<Disponibilidad>()
                        .HasOne(d => d.Profesional)
                        .WithMany()
                        .HasForeignKey(d => d.Id_Profesional)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

