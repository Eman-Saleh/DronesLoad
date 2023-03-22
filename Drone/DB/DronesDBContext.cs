using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DronesLoad.DB
{
    public partial class DronesDBContext : DbContext
    {
        public DronesDBContext()
        {
        }

        public DronesDBContext(DbContextOptions<DronesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drone> Drones { get; set; } = null!;
        public virtual DbSet<DroneModel> DroneModels { get; set; } = null!;
        public virtual DbSet<DroneState> DroneStates { get; set; } = null!;
        public virtual DbSet<Medication> Medications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drone>(entity =>
            {
                entity.ToTable("Drone");

                entity.HasIndex(e => e.SerialNumber, "UQ__Drone__048A00082C70192C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatteryCapacity).HasColumnName("batteryCapacity");

                entity.Property(e => e.ModelId).HasColumnName("modelID");

                entity.Property(e => e.SerialNumber).HasMaxLength(100);

                entity.Property(e => e.StateId).HasColumnName("stateID");

                entity.Property(e => e.WeightLimit).HasColumnName("weightLimit");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Drones)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Drone_Model");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Drones)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Drone_State");
            });

            modelBuilder.Entity<DroneModel>(entity =>
            {
                entity.ToTable("DroneModel");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ModelName).HasMaxLength(20);
            });

            modelBuilder.Entity<DroneState>(entity =>
            {
                entity.ToTable("DroneState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .HasMaxLength(10)
                    .HasColumnName("stateName");
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.ToTable("Medication");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DroneId).HasColumnName("DroneID");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Weight).HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.Drone)
                    .WithMany(p => p.Medications)
                    .HasForeignKey(d => d.DroneId)
                    .HasConstraintName("FK_Medication_Drone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
