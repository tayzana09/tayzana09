using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test1.Model
{
    public partial class KrutestContext : DbContext
    {
        public KrutestContext()
        {
        }

        public KrutestContext(DbContextOptions<KrutestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TableCategory> TableCategorys { get; set; }
        public virtual DbSet<TableCustomer> TableCustomers { get; set; }
        public virtual DbSet<TableProduct> TableProducts { get; set; }
        public virtual DbSet<TableTitleName> TableTitleNames { get; set; }
        public virtual DbSet<TableUnit> TableUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableCategory>(entity =>
            {
                entity.Property(e => e.CatId)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CatName).IsUnicode(false);
            });

            modelBuilder.Entity<TableCustomer>(entity =>
            {
                entity.Property(e => e.CustId)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CustType).IsUnicode(false);

                entity.Property(e => e.InitialCode).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.InitialCodeNavigation)
                    .WithMany(p => p.TableCustomers)
                    .HasForeignKey(d => d.InitialCode)
                    .HasConstraintName("FK_Table_Customer_ToTable");
            });

            modelBuilder.Entity<TableProduct>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CatId).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UnitCode).IsUnicode(false);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.TableProducts)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_Table_Product_ToTable");
            });

            modelBuilder.Entity<TableTitleName>(entity =>
            {
                entity.Property(e => e.InitialCode)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.InitialName).IsUnicode(false);
            });

            modelBuilder.Entity<TableUnit>(entity =>
            {
                entity.Property(e => e.UnitCode)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.UnitName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
