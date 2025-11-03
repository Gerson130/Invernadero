using Microsoft.EntityFrameworkCore;
using Invernadero.Models;

namespace Invernadero.Models
{
    public class InvernaderoContext : DbContext
    {
        public InvernaderoContext(DbContextOptions<InvernaderoContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<Registro> Registro { get; set; }
        public DbSet<CalendarioDeRiego> CalendarioDeRiego { get; set; }
        public DbSet<Notifica> Notifica { get; set; }
        public DbSet<UsuarioSensor> UsuarioSensor { get; set; }
        public DbSet<RegistroSensor> RegistroSensor { get; set; }
        public DbSet<CalendarioDeRiegoUsuario> CalendarioDeRiegoUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entidad =>
            {
                entidad.ToTable("Usuario");
                entidad.HasKey(e => e.Id);
                entidad.Property(e => e.Id).HasMaxLength(9);
                entidad.Property(e => e.Nombre).IsRequired().HasMaxLength(20).IsUnicode(false);
                entidad.Property(e => e.Contrasenia).IsRequired().HasMaxLength(9).IsUnicode(true);
            });
            modelBuilder.Entity<Registro>(entidad =>
            {
                entidad.ToTable("Registro");
                entidad.HasKey(e => e.Id);
                entidad.Property(e => e.Id).HasMaxLength(9);
                entidad.Property(e => e.Humedad).IsRequired().HasMaxLength(4);
                entidad.Property(e => e.Temperatura).IsRequired().HasMaxLength(4);
            });
            modelBuilder.Entity<CalendarioDeRiego>(entidad =>
            {
                entidad.ToTable("CalendarioRiego");
                entidad.HasKey(e => e.Id);
                entidad.Property(e => e.Id).HasMaxLength(9);
                entidad.Property(e => e.Tipo).IsRequired().HasMaxLength(22);
                entidad.Property(e => e.NivelAgua).IsRequired().HasMaxLength(4);
            });
            modelBuilder.Entity<Sensor>(entidad =>
            {
                entidad.ToTable("Sensor");
                entidad.HasKey(e => e.Id);
                entidad.Property(e => e.Nombre).IsRequired().HasMaxLength(15);
                entidad.Property(e => e.Ubicacion).IsRequired().HasMaxLength(12);
            });
            modelBuilder.Entity<Notifica>(entidad =>
            {
                entidad.ToTable("Notifica");
                entidad.HasKey(me => new { me.IdCalendarioRiego });
                entidad.HasOne(me => me.CalendarioRiego).WithMany(m => m.Notificacion).HasForeignKey(me => me.IdCalendarioRiego);
                entidad.HasOne(me => me.Usuario).WithMany(m => m.Notificacion).HasForeignKey(me => me.IdUsuario);
            });
            modelBuilder.Entity<UsuarioSensor>(entidad =>
            {
                entidad.ToTable("RegistroConfiguración");
                entidad.HasKey(me => new { me.IdUsuario, me.IdSensor });
                entidad.HasOne(me => me.Usuario).WithMany(m => m.UsuarioSensor).HasForeignKey(me => me.IdUsuario);
                entidad.HasOne(me => me.Sensor).WithMany(m => m.UsuarioSensor).HasForeignKey(me => me.IdSensor);
            });
            modelBuilder.Entity<RegistroSensor>(entidad =>
            {
                entidad.ToTable("RegistroSensor");
                entidad.HasKey(me => new { me.IdRegistro, me.IdSensor });
                entidad.HasOne(me => me.Registro).WithMany(m => m.RegistroSensor).HasForeignKey(me => me.IdRegistro);
                entidad.HasOne(me => me.Sensor).WithMany(m => m.RegistroSensor).HasForeignKey(me => me.IdSensor);
            });
            modelBuilder.Entity<CalendarioDeRiegoUsuario>(entidad =>
            {
                entidad.ToTable("RegistroCalendario");
                entidad.HasKey(me => new { me.IdCalendarioDeRiego, me.IdUsuario });
                entidad.HasOne(me => me.CalendarioDeRiego).WithMany(m => m.CalendarioRiegoUsuario).HasForeignKey(me => me.IdCalendarioDeRiego);
                entidad.HasOne(me => me.Usuario).WithMany(m => m.CalendarioRiegoUsuario).HasForeignKey(me => me.IdUsuario);
            });

        }
    }
}
