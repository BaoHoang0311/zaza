using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Service.Domain.Models;

namespace Service.Domain.Infrastructure;

public partial class AestrainingContext : DbContext
{
    public AestrainingContext()
    {
    }

    public AestrainingContext(DbContextOptions<AestrainingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TransactionData> TransactionDatas { get; set; }

    public virtual DbSet<TransactionGcedata> TransactionGcedatas { get; set; }

    public virtual DbSet<TransactionGcrdata> TransactionGcrdatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Manufacturer).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionData>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("PK__Transact__9E5DDB1C8609EDB4");

            entity.Property(e => e.TransId)
                .ValueGeneratedNever()
                .HasColumnName("TransID");
            entity.Property(e => e.ClosingAgtBizName).HasMaxLength(456);
            entity.Property(e => e.ClosingAgtCeano)
                .HasMaxLength(50)
                .HasColumnName("ClosingAgtCEANo");
            entity.Property(e => e.CultureCode).HasMaxLength(10);
            entity.Property(e => e.CurrencyType).HasMaxLength(10);
            entity.Property(e => e.GeneratedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HseNo).HasMaxLength(100);
            entity.Property(e => e.KeyinDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdater).HasMaxLength(100);
            entity.Property(e => e.OptionDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectName).HasMaxLength(456);
            entity.Property(e => e.PropertyCategory).HasMaxLength(50);
            entity.Property(e => e.PropertyCategoryId).HasColumnName("PropertyCategoryID");
            entity.Property(e => e.PropertyModel).HasMaxLength(100);
            entity.Property(e => e.PropertyType).HasMaxLength(50);
            entity.Property(e => e.SalesType).HasMaxLength(50);
            entity.Property(e => e.StreetName).HasMaxLength(456);
            entity.Property(e => e.SubDate).HasColumnType("datetime");
            entity.Property(e => e.TransType).HasMaxLength(50);
            entity.Property(e => e.TransTypeId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("TransTypeID");
        });

        modelBuilder.Entity<TransactionGcedata>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC273B03EC79");

            entity.ToTable("TransactionGCEDatas");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgtBizName).HasMaxLength(456);
            entity.Property(e => e.AgtCeano)
                .HasMaxLength(50)
                .HasColumnName("AgtCEANo");
            entity.Property(e => e.CategoryDisplay).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.KeyinDate).HasColumnType("datetime");
            entity.Property(e => e.OptionDate).HasColumnType("datetime");
            entity.Property(e => e.RoleDisplay).HasMaxLength(100);
            entity.Property(e => e.SubDate).HasColumnType("datetime");
            entity.Property(e => e.TransId).HasColumnName("TransID");
            entity.Property(e => e.TypeId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("TypeID");
        });

        modelBuilder.Entity<TransactionGcrdata>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC273738F2EE");

            entity.ToTable("TransactionGCRDatas");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgtBizName).HasMaxLength(456);
            entity.Property(e => e.AgtCeano)
                .HasMaxLength(50)
                .HasColumnName("AgtCEANo");
            entity.Property(e => e.CategoryDisplay).HasMaxLength(100);
            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("CategoryID");
            entity.Property(e => e.RcvDate).HasColumnType("datetime");
            entity.Property(e => e.RoleDisplay).HasMaxLength(100);
            entity.Property(e => e.TransId).HasColumnName("TransID");
            entity.Property(e => e.TransSource).HasMaxLength(10);
            entity.Property(e => e.TypeId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("TypeID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
