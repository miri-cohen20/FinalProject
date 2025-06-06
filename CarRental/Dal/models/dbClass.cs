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

    public virtual DbSet<Renting> Rentings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\User\\Desktop\\הנדסאים\\try project\\FinalProject\\CarRental\\Dal\\Data\\database.mdf';Integrated Security=True;Connect Timeout=30;Encrypt=True;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC079B5127FB");

            entity.ToTable("Car");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("city");
            entity.Property(e => e.CleanStatus).HasColumnName("cleanStatus");
            entity.Property(e => e.DescriptionCleaning)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("descriptionCleaning");
            entity.Property(e => e.DescriptionProper)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("descriptionProper");
            entity.Property(e => e.FinalCleaning).HasColumnType("datetime");
            entity.Property(e => e.LastCorrection).HasColumnType("datetime");
            entity.Property(e => e.ProperStatus).HasColumnName("properStatus");
            entity.Property(e => e.Seats).HasColumnName("seats");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("street");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0774875EA2");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

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

        modelBuilder.Entity<Renting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A6B650C1");

            entity.ToTable("Renting");

            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.IdCar)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
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

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
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
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC074DD69D3F");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.BuildingNumber).HasColumnName("buildingNumber");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("password");
            entity.Property(e => e.PhonNumber)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("phonNumber");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("street");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07D0D6F053");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Price).HasColumnName("price");

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
