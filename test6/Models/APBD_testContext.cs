using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace test6.Models
{
    public partial class APBD_testContext : DbContext
    {
        public APBD_testContext()
        {
        }

        public APBD_testContext(DbContextOptions<APBD_testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Table1> Table1 { get; set; }
        public virtual DbSet<Table2> Table2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4S302R6\\SQLEXPRESS;Database=APBD_test;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table1>(entity =>
            {
                entity.HasKey(e => e.Idaaa);

                entity.ToTable("Table_1");

                entity.Property(e => e.Idaaa).HasColumnName("idaaa");

                entity.Property(e => e.Sdsd)
                    .IsRequired()
                    .HasColumnName("sdsd")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Table2>(entity =>
            {
                entity.HasKey(e => e.Intnew);

                entity.ToTable("Table_2");

                entity.HasIndex(e => e.Intnew)
                    .HasName("IX_Table_2");

                entity.Property(e => e.Intnew)
                    .HasColumnName("intnew")
                    .ValueGeneratedNever();

                entity.Property(e => e.Idaa).HasColumnName("idaa");

                entity.HasOne(d => d.IdaaNavigation)
                    .WithMany(p => p.Table2)
                    .HasForeignKey(d => d.Idaa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_2_Table_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
