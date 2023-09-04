using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ASPIspit.DataAccess.Models;

#nullable disable

namespace ASPIspit.DataAccess.Data
{
    public partial class AspIspitContext : DbContext
    {
        public AspIspitContext()
        {
        }

        public AspIspitContext(DbContextOptions<AspIspitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<ManufacturersVehicleType> ManufacturersVehicleTypes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<RegistrationPlate> RegistrationPlates { get; set; }
        public virtual DbSet<ServiceSchedule> ServiceSchedules { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserUseCase> UserUseCases { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-H4F32AV\\SQLEXPRESS;Initial Catalog=AspIspit;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Manufacturers")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ManufacturersVehicleType>(entity =>
            {
                entity.HasKey(e => new { e.VehicleTypeId, e.ManufacturerId });

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.ManufacturersVehicleTypes)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManufacturersVehicleTypes_Manufacturers");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.ManufacturersVehicleTypes)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManufacturersVehicleTypes_VehicleTypes");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Models_Manufacturers");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Models_VehicleTypes");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.Property(e => e.IssuedAt).HasColumnType("datetime");

                entity.Property(e => e.ValidUntil).HasColumnType("datetime");

                entity.HasOne(d => d.RegistrationPlate)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.RegistrationPlateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registrations_RegistrationPlates");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registrations_Vehicles");
            });

            modelBuilder.Entity<RegistrationPlate>(entity =>
            {
                entity.HasIndex(e => e.RegistrationPlate1, "IX_RegistrationPlates")
                    .IsUnique();

                entity.Property(e => e.RegistrationPlate1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("RegistrationPlate");
            });

            modelBuilder.Entity<ServiceSchedule>(entity =>
            {
                entity.Property(e => e.AdditionalInfo).HasMaxLength(500);

                entity.Property(e => e.FinishedAt).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ScheduledFor).HasColumnType("datetime");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.ServiceSchedules)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceSchedules_ServiceTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.ServiceSchedules)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceSchedules_Vehicles");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_ServiceTypes")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "IX_Users")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<UserUseCase>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.UseCaseId });

                entity.ToTable("UserUseCase");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserUseCases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserUseCase_Users");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.Label, "NonClusteredIndex-20210624-185746");

                entity.Property(e => e.Label).HasMaxLength(100);

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Models");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_VehicleTypes")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
