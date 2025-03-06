using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MasterPol.Models;

public partial class _43pKasatkinaUpContext : DbContext
{
    public _43pKasatkinaUpContext()
    {
    }

    public _43pKasatkinaUpContext(DbContextOptions<_43pKasatkinaUpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<PartnersProduct> PartnersProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=43P_Kasatkina_UP;Username=postgres;Password=kasatkina78");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materials_pkey");

            entity.ToTable("materials");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DefectPercentage)
                .HasPrecision(10, 6)
                .HasColumnName("defect_percentage");
            entity.Property(e => e.MaterialType)
                .HasMaxLength(50)
                .HasColumnName("material_type");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partners_pkey");

            entity.ToTable("partners");

            entity.HasIndex(e => e.Email, "partners_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DirectorName)
                .HasMaxLength(255)
                .HasColumnName("director_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Inn).HasColumnName("inn");
            entity.Property(e => e.LegalAddress).HasColumnName("legal_address");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo");
            entity.Property(e => e.PartnerName)
                .HasMaxLength(255)
                .HasColumnName("partner_name");
            entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.PartnerType).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerTypeId)
                .HasConstraintName("partners_partner_type_id_fkey");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partner_types_pkey");

            entity.ToTable("partner_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<PartnersProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partners_products_pkey");

            entity.ToTable("partners_products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");

            entity.HasOne(d => d.Partner).WithMany(p => p.PartnersProducts)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("partners_products_partner_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.PartnersProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("partners_products_product_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticleNumber)
                .HasMaxLength(8)
                .HasColumnName("article_number");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.MinPartnerPrice)
                .HasPrecision(10, 2)
                .HasColumnName("min_partner_price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("products_product_type_id_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_types_pkey");

            entity.ToTable("product_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coefficient)
                .HasPrecision(10, 2)
                .HasColumnName("coefficient");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
