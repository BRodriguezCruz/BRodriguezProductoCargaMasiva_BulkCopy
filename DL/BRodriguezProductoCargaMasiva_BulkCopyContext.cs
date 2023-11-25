using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class BRodriguezProductoCargaMasiva_BulkCopyContext : DbContext
    {
        public BRodriguezProductoCargaMasiva_BulkCopyContext()
        {
        }

        public BRodriguezProductoCargaMasiva_BulkCopyContext(DbContextOptions<BRodriguezProductoCargaMasiva_BulkCopyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-6OBJBAUI; Database= BRodriguezProductoCargaMasiva_BulkCopy; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProdcuto)
                    .HasName("PK__Producto__1CFBE03AD84B127C");

                entity.ToTable("Producto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitario).HasColumnType("smallmoney");

                entity.HasOne(d => d.IdProovedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProovedor)
                    .HasConstraintName("FK__Producto__IdProo__1273C1CD");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProvedor)
                    .HasName("PK__Proveedo__AEBD4C185E2BC442");

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdProvedor).ValueGeneratedNever();

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Proveedor1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Proveedor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
