﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodMateApp.Models
{
    public partial class FOODMATEContext : DbContext
    {
        public FOODMATEContext()
        {
        }

        public FOODMATEContext(DbContextOptions<FOODMATEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lifts> Lifts { get; set; }
        public virtual DbSet<Meals> Meals { get; set; }
        public virtual DbSet<Measurements> Measurements { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=KACPER;Database=FOODMATE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lifts>(entity =>
            {
                entity.HasKey(e => e.LiftId)
                    .HasName("PK__lifts__D9C8A289337D7016");

                entity.Property(e => e.LiftId).HasColumnName("lift_id");

                entity.Property(e => e.LiftName)
                    .IsRequired()
                    .HasColumnName("lift_name")
                    .HasMaxLength(8);

                entity.Property(e => e.MDate)
                    .HasColumnName("m_date")
                    .HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Value).HasColumnName("value");

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

                entity.Property(e => e.MealId).HasColumnName("meal_id");

                entity.Property(e => e.MDate)
                    .HasColumnName("m_date")
                    .HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

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

                entity.Property(e => e.MeasurementId).HasColumnName("measurement_id");

                entity.Property(e => e.Butt).HasColumnName("butt");

                entity.Property(e => e.Chest).HasColumnName("chest");

                entity.Property(e => e.LArm).HasColumnName("l_arm");

                entity.Property(e => e.LCalve).HasColumnName("l_calve");

                entity.Property(e => e.LForearm).HasColumnName("l_forearm");

                entity.Property(e => e.LThigh).HasColumnName("l_thigh");

                entity.Property(e => e.MDate)
                    .HasColumnName("m_date")
                    .HasColumnType("date");

                entity.Property(e => e.RArm).HasColumnName("r_arm");

                entity.Property(e => e.RCalve).HasColumnName("r_calve");

                entity.Property(e => e.RForearm).HasColumnName("r_forearm");

                entity.Property(e => e.RThigh).HasColumnName("r_thigh");

                entity.Property(e => e.UWeight).HasColumnName("u_weight");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Waist).HasColumnName("waist");

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

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Carbs).HasColumnName("carbs");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(20);

                entity.Property(e => e.Fats).HasColumnName("fats");

                entity.Property(e => e.Kcal).HasColumnName("kcal");

                entity.Property(e => e.ProductNameEng)
                    .HasColumnName("product_name_eng")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNamePl)
                    .HasColumnName("product_name_pl")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Protein).HasColumnName("protein");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__users__B9BE370F38AA58BF");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Bench).HasColumnName("bench");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.Carbs).HasColumnName("carbs");

                entity.Property(e => e.Deadlift).HasColumnName("deadlift");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Fats).HasColumnName("fats");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(20);

                entity.Property(e => e.Kcal).HasColumnName("kcal");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(20);

                entity.Property(e => e.Measurements).HasColumnName("measurements");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasMaxLength(20);

                entity.Property(e => e.Ohp).HasColumnName("ohp");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.Pswd)
                    .HasColumnName("pswd")
                    .HasMaxLength(100);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasMaxLength(1);

                entity.Property(e => e.Squat).HasColumnName("squat");

                entity.Property(e => e.UHeight).HasColumnName("u_height");

                entity.Property(e => e.UType)
                    .HasColumnName("u_type")
                    .HasMaxLength(20);

                entity.Property(e => e.UWeight).HasColumnName("u_weight");

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
