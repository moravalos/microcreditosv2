using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MicroCreditosLolaApi.Models;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Deudore> Deudores { get; set; }

    public virtual DbSet<Historial> Historials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //      => optionsBuilder.UseSqlServer("server=LAPTOP-DQEPM0TK\\MORAVALOS;database=prueba; integrated security=true; Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Idcliente).HasName("PK__clientes__7B86132F3E1E624E");

            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Apellidom)
                .HasMaxLength(50)
                .HasColumnName("apellidom");
            entity.Property(e => e.Apellidop)
                .HasMaxLength(50)
                .HasColumnName("apellidop");
            entity.Property(e => e.Cantidadp)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("cantidadp");
            entity.Property(e => e.Diacobro).HasColumnName("diacobro");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Fechap)
                .HasColumnType("datetime")
                .HasColumnName("fechap");
            entity.Property(e => e.Intereses).HasColumnName("intereses");
            entity.Property(e => e.Mesesprestamo).HasColumnName("mesesprestamo");
            entity.Property(e => e.Montodebe)
                .HasComputedColumnSql("(([cantidadp]/[mesesprestamo]+([cantidadp]*[intereses])/(100))*[mesesprestamo])", false)
                .HasColumnType("decimal(35, 13)")
                .HasColumnName("montodebe");
            entity.Property(e => e.Montofinal)
                .HasComputedColumnSql("(([cantidadp]/[mesesprestamo]+([cantidadp]*[intereses])/(100))*[mesesprestamo])", false)
                .HasColumnType("decimal(35, 13)")
                .HasColumnName("montofinal");
            entity.Property(e => e.Montopagado)
                .HasComputedColumnSql("((0))", false)
                .HasColumnName("montopagado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(32)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Deudore>(entity =>
        {
            entity.HasKey(e => e.Iddeudor).HasName("PK__deudores__8FA0315A79B1C764");

            entity.ToTable("deudores");

            entity.Property(e => e.Iddeudor).HasColumnName("iddeudor");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Montodebe)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("montodebe");
            entity.Property(e => e.Montofinal)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("montofinal");
            entity.Property(e => e.Montopagado)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("montopagado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Deudores)
                .HasForeignKey(d => d.Idcliente)
                .HasConstraintName("fk_clientes");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.Idmonto).HasName("PK__historia__5F3A94CC55D0CCB0");

            entity.ToTable("historial");

            entity.Property(e => e.Idmonto).HasColumnName("idmonto");
            entity.Property(e => e.Fechadepago)
                .HasColumnType("date")
                .HasColumnName("fechadepago");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("monto");
            entity.Property(e => e.Periodopago).HasColumnName("periodopago");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.Idcliente)
                .HasConstraintName("fk_clientes2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
