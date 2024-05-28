using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.Models;

public partial class PazSalvoContext : DbContext
{
    public PazSalvoContext()
    {
    }

    public PazSalvoContext(DbContextOptions<PazSalvoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesDeFactura> DetallesDeFacturas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MediosDePago> MediosDePagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC0768B1D232");

            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Clientes__Person__59FA5E80");

            entity.HasOne(d => d.Rol).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Clientes__RolId__5AEE82B9");
        });

        modelBuilder.Entity<DetallesDeFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3214EC07DE5B9E42");

            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Factura).WithMany(p => p.DetallesDeFacturas)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__DetallesD__Factu__6E01572D");

            entity.HasOne(d => d.Pago).WithMany(p => p.DetallesDeFacturas)
                .HasForeignKey(d => d.PagoId)
                .HasConstraintName("FK__DetallesD__PagoI__6EF57B66");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC07800A561E");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3214EC07BC5E51CA");

            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Saldo)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Facturas__Client__619B8048");

            entity.HasOne(d => d.Estado).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK__Facturas__Estado__6477ECF3");

            entity.HasOne(d => d.MedioDePago).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.MedioDePagoId)
                .HasConstraintName("FK__Facturas__MedioD__6383C8BA");

            entity.HasOne(d => d.ServicioAdquirido).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ServicioAdquiridoId)
                .HasConstraintName("FK__Facturas__Servic__628FA481");
        });

        modelBuilder.Entity<MediosDePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MediosDe__3214EC072DC27202");

            entity.ToTable("MediosDePago");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC078704C71A");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoDePago)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Factura).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__Pagos__FacturaId__693CA210");

            
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC07959AA70C");

            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.DocumentoIdentificacion).HasMaxLength(20);
            
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07897A08BD");

            entity.Property(e => e.Activo).HasDefaultValueSql("((0))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC0715665506");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC075BD745D6");

            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);

            entity.HasOne(d => d.Persona).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Usuarios__Person__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
