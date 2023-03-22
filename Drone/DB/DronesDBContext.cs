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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
              
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drone>(entity =>
            {
                entity.ToTable("Drone");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatteryCapacity)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("batteryCapacity");

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

                entity.Property(e => e.ModelName).HasMaxLength(10);
            });

            modelBuilder.Entity<DroneState>(entity =>
            {
                entity.ToTable("DroneState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .HasMaxLength(10)
                    .HasColumnName("stateName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
