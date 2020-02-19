using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace foodmateapp.Model
{
    public partial class FoodmateContext : DbContext
    {
        public FoodmateContext()
        {
        }

        public FoodmateContext(DbContextOptions<FoodmateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lifts> Lifts { get; set; }
        public virtual DbSet<Meals> Meals { get; set; }
        public virtual DbSet<Measurements> Measurements { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=KACPER;Database=FoodMate;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lifts>(entity =>
            {
                entity.HasKey(e => e.LiftId)
                    .HasName("PK__lifts__D9C8A289337D7016");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lifts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__lifts__user_id__5441852A");
            });

            modelBuilder.Entity<Meals>(entity =>
            {
                entity.HasKey(e => e.MealId)
                    .HasName("PK__meals__2910B00F78701AE6");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__meals__product_i__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__meals__user_id__693CA210");
            });

            modelBuilder.Entity<Measurements>(entity =>
            {
                entity.HasKey(e => e.MeasurementId)
                    .HasName("PK__measurem__E3D1E1C1D6C54877");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MeasurementsNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__measureme__user___4D94879B");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__products__47027DF5F1DA5EBE");

                entity.Property(e => e.ProductNameEng).IsUnicode(false);

                entity.Property(e => e.ProductNamePl).IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__users__B9BE370F38AA58BF");

                entity.HasOne(d => d.BenchNavigation)
                    .WithMany(p => p.UsersBenchNavigation)
                    .HasForeignKey(d => d.Bench)
                    .HasConstraintName("FK__users__bench__5629CD9C");

                entity.HasOne(d => d.DeadliftNavigation)
                    .WithMany(p => p.UsersDeadliftNavigation)
                    .HasForeignKey(d => d.Deadlift)
                    .HasConstraintName("FK__users__deadlift__59063A47");

                entity.HasOne(d => d.Measurements1)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Measurements)
                    .HasConstraintName("FK__users__measureme__5535A963");

                entity.HasOne(d => d.OhpNavigation)
                    .WithMany(p => p.UsersOhpNavigation)
                    .HasForeignKey(d => d.Ohp)
                    .HasConstraintName("FK__users__ohp__571DF1D5");

                entity.HasOne(d => d.SquatNavigation)
                    .WithMany(p => p.UsersSquatNavigation)
                    .HasForeignKey(d => d.Squat)
                    .HasConstraintName("FK__users__squat__5812160E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
