using System;
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
                    .HasName("PK__Account__1788CCAC87746E84");

                entity.ToTable("Account");

                entity.HasIndex(e => e.Email, "UQ__Account__A9D1053400C63BE1")
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
                    .HasName("PK__Ads_Pack__0CC88B4B1AFEE2B0");

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
                    .HasName("PK__Advertis__7130D58EEEE81931");

                entity.ToTable("Advertisement");

                entity.Property(e => e.AdId)
                    .HasMaxLength(50)
                    .HasColumnName("AdID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Element).HasMaxLength(50);

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

                entity.HasOne(d => d.ElementNavigation)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.Element)
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
                entity.HasKey(e => e.Color1)
                    .HasName("PK__Color__E11D38445F7BF20C");

                entity.ToTable("Color");

                entity.Property(e => e.Color1)
                    .HasMaxLength(50)
                    .HasColumnName("Color");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasKey(e => e.Direction1)
                    .HasName("PK__Directio__2714D390434D4D6D");

                entity.ToTable("Direction");

                entity.Property(e => e.Direction1)
                    .HasMaxLength(50)
                    .HasColumnName("Direction");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.Element1)
                    .HasName("PK__Element__4428BD16FA2DF882");

                entity.ToTable("Element");

                entity.Property(e => e.Element1)
                    .HasMaxLength(50)
                    .HasColumnName("Element");

                entity.Property(e => e.Mutualism).HasMaxLength(50);
            });

            modelBuilder.Entity<ElementColor>(entity =>
            {
                entity.HasKey(e => new { e.Element, e.Color })
                    .HasName("PK__Element___1A396E92E1BB5DA8");

                entity.ToTable("Element_Color");

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.HasOne(d => d.ColorNavigation)
                    .WithMany(p => p.ElementColors)
                    .HasForeignKey(d => d.Color)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element_C__Color__60A75C0F");

                entity.HasOne(d => d.ElementNavigation)
                    .WithMany(p => p.ElementColors)
                    .HasForeignKey(d => d.Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element_C__Eleme__5FB337D6");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FbId)
                    .HasName("PK__Feedback__36769D6CF6285CB2");

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
                    .HasName("PK__Koi_Vari__B9D5579B41C2EA78");

                entity.ToTable("Koi_Variety");

                entity.Property(e => e.KoiType).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(255);
            });

            modelBuilder.Entity<LifePalace>(entity =>
            {
                entity.HasKey(e => e.LifePalace1)
                    .HasName("PK__LifePala__5E226587DDFBF18C");

                entity.ToTable("LifePalace");

                entity.Property(e => e.LifePalace1)
                    .HasMaxLength(50)
                    .HasColumnName("LifePalace");
            });

            modelBuilder.Entity<LifePalaceDirection>(entity =>
            {
                entity.HasKey(e => new { e.LifePalace, e.Direction })
                    .HasName("PK__LifePala__4C5328BE06A0587E");

                entity.ToTable("LifePalace_Direction");

                entity.Property(e => e.LifePalace).HasMaxLength(50);

                entity.Property(e => e.Direction).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EightMansions).HasMaxLength(50);

                entity.HasOne(d => d.DirectionNavigation)
                    .WithMany(p => p.LifePalaceDirections)
                    .HasForeignKey(d => d.Direction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LifePalac__Direc__68487DD7");

                entity.HasOne(d => d.LifePalaceNavigation)
                    .WithMany(p => p.LifePalaceDirections)
                    .HasForeignKey(d => d.LifePalace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LifePalac__LifeP__6754599E");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Member__1788CCAC80C33D5C");

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
                    .HasName("PK__Package__DF85EC568A15E521");

                entity.ToTable("Package");

                entity.Property(e => e.Rank).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<PointOfShape>(entity =>
            {
                entity.HasKey(e => new { e.Element, e.Shape })
                    .HasName("PK__PointOfS__14FECAE665F6E8C6");

                entity.ToTable("PointOfShape");

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Shape).HasMaxLength(50);

                entity.Property(e => e.PointOfShape1).HasColumnName("PointOfShape");

                entity.HasOne(d => d.ElementNavigation)
                    .WithMany(p => p.PointOfShapes)
                    .HasForeignKey(d => d.Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PointOfSh__Eleme__5441852A");

                entity.HasOne(d => d.ShapeNavigation)
                    .WithMany(p => p.PointOfShapes)
                    .HasForeignKey(d => d.Shape)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PointOfSh__Shape__5535A963");
            });

            modelBuilder.Entity<QuantityOfFish>(entity =>
            {
                entity.HasKey(e => e.Element)
                    .HasName("PK__Quantity__4428BD1603453CB9");

                entity.ToTable("QuantityOfFish");

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.ElementNavigation)
                    .WithOne(p => p.QuantityOfFish)
                    .HasForeignKey<QuantityOfFish>(d => d.Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuantityO__Eleme__5165187F");
            });

            modelBuilder.Entity<Shape>(entity =>
            {
                entity.HasKey(e => e.Shape1)
                    .HasName("PK__Shape__0D677F0443BF929F");

                entity.ToTable("Shape");

                entity.Property(e => e.Shape1)
                    .HasMaxLength(50)
                    .HasColumnName("Shape");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image");
            });

            modelBuilder.Entity<TypeColor>(entity =>
            {
                entity.HasKey(e => new { e.KoiType, e.Color })
                    .HasName("PK__Type_Col__E7C4841F8C93FCE9");

                entity.ToTable("Type_Color");

                entity.Property(e => e.KoiType).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.HasOne(d => d.ColorNavigation)
                    .WithMany(p => p.TypeColors)
                    .HasForeignKey(d => d.Color)
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
