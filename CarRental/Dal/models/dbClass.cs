using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.models;

public partial class dbClass : DbContext
{
    public dbClass()
    {
    }

    public dbClass(DbContextOptions<dbClass> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<RentalOrder> RentalOrders { get; set; }

    public virtual DbSet<Renting> Rentings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='H:\\Project\\CarRental\\Dal\\Data\\database.mdf';Integrated Security=True;Connect Timeout=30;Encrypt=True;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Car__3214EC07B082D67B");

            entity.ToTable("Car");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FinalCleaning).HasColumnType("datetime");
            entity.Property(e => e.LastCorrection).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__6037852F88FA4804");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_ToTable");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC075C0EDBA9");

            entity.ToTable("Price");

            entity.Property(e => e.PriceForHour).HasColumnName("priceForHour");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<RentalOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RentalOr__3214EC07AFC6C64C");

            entity.ToTable("RentalOrder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Car).HasColumnName("car");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TimeOfDay).HasColumnName("timeOfDay");
        });

        modelBuilder.Entity<Renting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A6B650C1");

            entity.ToTable("Renting");

            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RentalTime).HasColumnType("datetime");
            entity.Property(e => e.ReturnTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.Rentings)
                .HasForeignKey(d => d.IdCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Renting_Car");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Rentings)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Renting_Customers");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07808DA9D8");

            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC070CCDE39F");

            entity.ToTable("Table");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Time__3214EC07E220D8A2");

            entity.ToTable("Time");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Duration).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC070F5D6544");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BuildingNumber).HasColumnName("buildingNumber");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.PhonNumber).HasColumnName("phonNumber");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071E1A4C2B");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Worker)
                .HasForeignKey<Worker>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workers_ToTable");

            entity.HasOne(d => d.Rols).WithMany(p => p.Workers)
                .HasForeignKey(d => d.RolsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workers_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
