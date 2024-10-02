﻿using System;
using System.Collections.Generic;
using FengShuiKoi_BO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FengShuiKoi_DAO
{
    public partial class SWP391_FengShuiKoiConsulting_DBContext : DbContext
    {
        public SWP391_FengShuiKoiConsulting_DBContext()
        {
        }

        public SWP391_FengShuiKoiConsulting_DBContext(DbContextOptions<SWP391_FengShuiKoiConsulting_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AdsPackage> AdsPackages { get; set; } = null!;
        public virtual DbSet<Advertisement> Advertisements { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Direction> Directions { get; set; } = null!;
        public virtual DbSet<Element> Elements { get; set; } = null!;
        public virtual DbSet<ElementColor> ElementColors { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<KoiVariety> KoiVarieties { get; set; } = null!;
        public virtual DbSet<LifePalace> LifePalaces { get; set; } = null!;
        public virtual DbSet<LifePalaceDirection> LifePalaceDirections { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<PointOfShape> PointOfShapes { get; set; } = null!;
        public virtual DbSet<QuantityOfFish> QuantityOfFishes { get; set; } = null!;
        public virtual DbSet<Shape> Shapes { get; set; } = null!;
        public virtual DbSet<TypeColor> TypeColors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HAUHANDSOME\\HAUHUYNH;Database=SWP391_FengShuiKoiConsulting_DB;Uid=sa;Pwd=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Account__1788CCAC40C90F3E");

                entity.ToTable("Account");

                entity.HasIndex(e => e.Email, "UQ__Account__A9D10534B5B99EF7")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<AdsPackage>(entity =>
            {
                entity.HasKey(e => new { e.AdId, e.Rank })
                    .HasName("PK__Ads_Pack__0CC88B4B7558F130");

                entity.ToTable("Ads_Package");

                entity.Property(e => e.AdId)
                    .HasMaxLength(50)
                    .HasColumnName("AdID");

                entity.Property(e => e.Rank).HasMaxLength(50);

                entity.Property(e => e.ExpiredDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.AdsPackages)
                    .HasForeignKey(d => d.AdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ads_Packag__AdID__4BAC3F29");

                entity.HasOne(d => d.RankNavigation)
                    .WithMany(p => p.AdsPackages)
                    .HasForeignKey(d => d.Rank)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ads_Packag__Rank__4CA06362");
            });

            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.HasKey(e => e.AdId)
                    .HasName("PK__Advertis__7130D58E0D70406D");

                entity.ToTable("Advertisement");

                entity.Property(e => e.AdId)
                    .HasMaxLength(50)
                    .HasColumnName("AdID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ElementId)
                    .HasMaxLength(50)
                    .HasColumnName("ElementID");

                entity.Property(e => e.Heading).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.Rank).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.ElementId)
                    .HasConstraintName("FK__Advertise__Eleme__44FF419A");

                entity.HasOne(d => d.RankNavigation)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.Rank)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Advertisem__Rank__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Advertise__UserI__4316F928");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.BlogId)
                    .HasMaxLength(50)
                    .HasColumnName("BlogID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Heading).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.ColorId)
                    .HasMaxLength(50)
                    .HasColumnName("ColorID");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("Direction");

                entity.Property(e => e.DirectionId)
                    .HasMaxLength(50)
                    .HasColumnName("DirectionID");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.ToTable("Element");

                entity.Property(e => e.ElementId)
                    .HasMaxLength(50)
                    .HasColumnName("ElementID");

                entity.Property(e => e.Mutualism).HasMaxLength(50);
            });

            modelBuilder.Entity<ElementColor>(entity =>
            {
                entity.HasKey(e => new { e.ElementId, e.ColorId })
                    .HasName("PK__Element___6CF3044CEC7E0991");

                entity.ToTable("Element_Color");

                entity.Property(e => e.ElementId)
                    .HasMaxLength(50)
                    .HasColumnName("ElementID");

                entity.Property(e => e.ColorId)
                    .HasMaxLength(50)
                    .HasColumnName("ColorID");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ElementColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element_C__Color__60A75C0F");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.ElementColors)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element_C__Eleme__5FB337D6");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FbId)
                    .HasName("PK__Feedback__36769D6C34EBB518");

                entity.ToTable("Feedback");

                entity.Property(e => e.FbId)
                    .HasMaxLength(50)
                    .HasColumnName("FbID");

                entity.Property(e => e.AdId)
                    .HasMaxLength(50)
                    .HasColumnName("AdID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.AdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__AdID__47DBAE45");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__UserID__48CFD27E");
            });

            modelBuilder.Entity<KoiVariety>(entity =>
            {
                entity.HasKey(e => e.KoiType)
                    .HasName("PK__Koi_Vari__B9D5579B8DB77A24");

                entity.ToTable("Koi_Variety");

                entity.Property(e => e.KoiType).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(255);
            });

            modelBuilder.Entity<LifePalace>(entity =>
            {
                entity.ToTable("LifePalace");

                entity.Property(e => e.LifePalaceId)
                    .HasMaxLength(50)
                    .HasColumnName("LifePalaceID");
            });

            modelBuilder.Entity<LifePalaceDirection>(entity =>
            {
                entity.HasKey(e => new { e.LifePalaceId, e.DirectionId })
                    .HasName("PK__LifePala__A0F9BE3056604006");

                entity.ToTable("LifePalace_Direction");

                entity.Property(e => e.LifePalaceId)
                    .HasMaxLength(50)
                    .HasColumnName("LifePalaceID");

                entity.Property(e => e.DirectionId)
                    .HasMaxLength(50)
                    .HasColumnName("DirectionID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EightMansions).HasMaxLength(50);

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.LifePalaceDirections)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LifePalac__Direc__68487DD7");

                entity.HasOne(d => d.LifePalace)
                    .WithMany(p => p.LifePalaceDirections)
                    .HasForeignKey(d => d.LifePalaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LifePalac__LifeP__6754599E");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Member__1788CCACE0AF3467");

                entity.ToTable("Member");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Member)
                    .HasForeignKey<Member>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Member__UserID__3A81B327");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.Rank)
                    .HasName("PK__Package__DF85EC56FC81B892");

                entity.ToTable("Package");

                entity.Property(e => e.Rank).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<PointOfShape>(entity =>
            {
                entity.HasKey(e => new { e.ElementId, e.ShapeId })
                    .HasName("PK__PointOfS__A326BA00947FDF2C");

                entity.ToTable("PointOfShape");

                entity.Property(e => e.ElementId)
                    .HasMaxLength(50)
                    .HasColumnName("ElementID");

                entity.Property(e => e.ShapeId)
                    .HasMaxLength(50)
                    .HasColumnName("ShapeID");

                entity.Property(e => e.PointOfShape1).HasColumnName("PointOfShape");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.PointOfShapes)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PointOfSh__Eleme__5441852A");

                entity.HasOne(d => d.Shape)
                    .WithMany(p => p.PointOfShapes)
                    .HasForeignKey(d => d.ShapeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PointOfSh__Shape__5535A963");
            });

            modelBuilder.Entity<QuantityOfFish>(entity =>
            {
                entity.HasKey(e => e.ElementId)
                    .HasName("PK__Quantity__A429723A29C0C277");

                entity.ToTable("QuantityOfFish");

                entity.Property(e => e.ElementId)
                    .HasMaxLength(50)
                    .HasColumnName("ElementID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.Element)
                    .WithOne(p => p.QuantityOfFish)
                    .HasForeignKey<QuantityOfFish>(d => d.ElementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuantityO__Eleme__5165187F");
            });

            modelBuilder.Entity<Shape>(entity =>
            {
                entity.ToTable("Shape");

                entity.Property(e => e.ShapeId)
                    .HasMaxLength(50)
                    .HasColumnName("ShapeID");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image");
            });

            modelBuilder.Entity<TypeColor>(entity =>
            {
                entity.HasKey(e => new { e.KoiType, e.ColorId })
                    .HasName("PK__Type_Col__710F21ED722ED5F9");

                entity.ToTable("Type_Color");

                entity.Property(e => e.KoiType).HasMaxLength(50);

                entity.Property(e => e.ColorId)
                    .HasMaxLength(50)
                    .HasColumnName("ColorID");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.TypeColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Type_Colo__Color__5CD6CB2B");

                entity.HasOne(d => d.KoiTypeNavigation)
                    .WithMany(p => p.TypeColors)
                    .HasForeignKey(d => d.KoiType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Type_Colo__KoiTy__5BE2A6F2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
