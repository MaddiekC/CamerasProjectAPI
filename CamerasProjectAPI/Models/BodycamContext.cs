using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CamerasProjectAPI.Models;

public partial class BodycamContext : DbContext
{
    public BodycamContext()
    {
    }

    public BodycamContext(DbContextOptions<BodycamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Camara> Camaras { get; set; }

    public virtual DbSet<Grabacione> Grabaciones { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Camara>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__camaras__3213E83F16AED604");

            entity.ToTable("camaras");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("activo")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ip");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            /*entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Camaras)
                .HasForeignKey(d => d.IdZona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__camaras__id_zona__20C1E124");*/
        });

        modelBuilder.Entity<Grabacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__grabacio__3213E83F3B94EB2E");

            entity.ToTable("grabaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("activo")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FinGrabacion)
                .HasColumnType("datetime")
                .HasColumnName("fin_grabacion");
            entity.Property(e => e.IdCamara).HasColumnName("id_camara");
            entity.Property(e => e.InicioGrabacion)
                .HasColumnType("datetime")
                .HasColumnName("inicio_grabacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UbicacionArchivo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ubicacion_archivo");

            /*entity.HasOne(d => d.IdCamaraNavigation).WithMany(p => p.Grabaciones)
                .HasForeignKey(d => d.IdCamara)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__grabacion__id_ca__21B6055D");*/
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permisos__3213E83FF6546F98");

            entity.ToTable("permisos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FA3F8B393");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles_pe__3213E83F1B184F3E");

            entity.ToTable("roles_permisos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPermisos).HasColumnName("id_permisos");
            entity.Property(e => e.IdRoles).HasColumnName("id_roles");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83FC21E435C");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("activo")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__zonas__3213E83FACE7DE06");

            entity.ToTable("zonas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("activo")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
