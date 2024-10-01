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
        public virtual DbSet<Pond> Ponds { get; set; } = null!;
        public virtual DbSet<Quantity> Quantities { get; set; } = null!;
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
                    .HasName("PK__Account__1788CCAC5C3D24BB");

                entity.ToTable("Account");

                entity.HasIndex(e => e.Email, "UQ__Account__A9D1053463E92ABD")
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
                    .HasName("PK__Ads_Pack__0CC88B4B0FC01EB7");

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
                    .HasConstraintName("FK__Ads_Packag__AdID__38996AB5");

                entity.HasOne(d => d.RankNavigation)
                    .WithMany(p => p.AdsPackages)
                    .HasForeignKey(d => d.Rank)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ads_Packag__Rank__398D8EEE");
            });

            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.HasKey(e => e.AdId)
                    .HasName("PK__Advertis__7130D58E9B21C501");

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
                    .HasConstraintName("FK__Advertise__Eleme__31EC6D26");

                entity.HasOne(d => d.RankNavigation)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.Rank)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Advertisem__Rank__30F848ED");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Advertise__UserI__300424B4");
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
                    .HasName("PK__Color__E11D38441D650002");

                entity.ToTable("Color");

                entity.Property(e => e.Color1)
                    .HasMaxLength(50)
                    .HasColumnName("Color");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasKey(e => e.Direction1)
                    .HasName("PK__Directio__2714D39074E97C5C");

                entity.ToTable("Direction");

                entity.Property(e => e.Direction1)
                    .HasMaxLength(50)
                    .HasColumnName("Direction");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.Element1)
                    .HasName("PK__Element__4428BD16D846E4F5");

                entity.ToTable("Element");

                entity.Property(e => e.Element1)
                    .HasMaxLength(50)
                    .HasColumnName("Element");

                entity.Property(e => e.Mutualism).HasMaxLength(50);
            });

            modelBuilder.Entity<ElementColor>(entity =>
            {
                entity.HasKey(e => new { e.Element, e.Color })
                    .HasName("PK__Element___1A396E92EACD9E62");

                entity.ToTable("Element_Color");

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.HasOne(d => d.ColorNavigation)
                    .WithMany(p => p.ElementColors)
                    .HasForeignKey(d => d.Color)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element_C__Color__534D60F1");

                entity.HasOne(d => d.ElementNavigation)
                    .WithMany(p => p.ElementColors)
                    .HasForeignKey(d => d.Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Element_C__Eleme__52593CB8");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FbId)
                    .HasName("PK__Feedback__36769D6CD83D14BC");

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
                    .HasConstraintName("FK__Feedback__AdID__34C8D9D1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__UserID__35BCFE0A");
            });

            modelBuilder.Entity<KoiVariety>(entity =>
            {
                entity.HasKey(e => e.KoiType)
                    .HasName("PK__Koi_Vari__B9D5579BD3A4F686");

                entity.ToTable("Koi_Variety");

                entity.Property(e => e.KoiType).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(255);
            });

            modelBuilder.Entity<LifePalace>(entity =>
            {
                entity.HasKey(e => e.LifePalace1)
                    .HasName("PK__LifePala__5E226587CABDDD0B");

                entity.ToTable("LifePalace");

                entity.Property(e => e.LifePalace1)
                    .HasMaxLength(50)
                    .HasColumnName("LifePalace");
            });

            modelBuilder.Entity<LifePalaceDirection>(entity =>
            {
                entity.HasKey(e => new { e.LifePalace, e.Direction })
                    .HasName("PK__LifePala__4C5328BEB01C6F9C");

                entity.ToTable("LifePalace_Direction");

                entity.Property(e => e.LifePalace).HasMaxLength(50);

                entity.Property(e => e.Direction).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EightMansions).HasMaxLength(50);

                entity.HasOne(d => d.DirectionNavigation)
                    .WithMany(p => p.LifePalaceDirections)
                    .HasForeignKey(d => d.Direction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LifePalac__Direc__5AEE82B9");

                entity.HasOne(d => d.LifePalaceNavigation)
                    .WithMany(p => p.LifePalaceDirections)
                    .HasForeignKey(d => d.LifePalace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LifePalac__LifeP__59FA5E80");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Member__1788CCACABCABB1B");

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
                    .HasConstraintName("FK__Member__UserID__276EDEB3");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.Rank)
                    .HasName("PK__Package__DF85EC56C11316DD");

                entity.ToTable("Package");

                entity.Property(e => e.Rank).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<PointOfShape>(entity =>
            {
                entity.HasKey(e => new { e.Element, e.Shape })
                    .HasName("PK__PointOfS__14FECAE62DE64C70");

                entity.ToTable("PointOfShape");

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Shape).HasMaxLength(50);

                entity.Property(e => e.PointOfShape1).HasColumnName("PointOfShape");

                entity.HasOne(d => d.ElementNavigation)
                    .WithMany(p => p.PointOfShapes)
                    .HasForeignKey(d => d.Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PointOfSh__Eleme__44FF419A");

                entity.HasOne(d => d.ShapeNavigation)
                    .WithMany(p => p.PointOfShapes)
                    .HasForeignKey(d => d.Shape)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PointOfSh__Shape__45F365D3");
            });

            modelBuilder.Entity<Pond>(entity =>
            {
                entity.ToTable("Pond");

                entity.Property(e => e.PondId)
                    .HasMaxLength(50)
                    .HasColumnName("PondID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Shape).HasMaxLength(50);
            });

            modelBuilder.Entity<Quantity>(entity =>
            {
                entity.HasKey(e => e.Element)
                    .HasName("PK__Quantity__4428BD162D02D479");

                entity.ToTable("Quantity");

                entity.Property(e => e.Element).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.ElementNavigation)
                    .WithOne(p => p.Quantity)
                    .HasForeignKey<Quantity>(d => d.Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quantity__Elemen__4222D4EF");
            });

            modelBuilder.Entity<Shape>(entity =>
            {
                entity.HasKey(e => e.Shape1)
                    .HasName("PK__Shape__0D677F04772A100A");

                entity.ToTable("Shape");

                entity.Property(e => e.Shape1)
                    .HasMaxLength(50)
                    .HasColumnName("Shape");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image");

                entity.HasMany(d => d.Elements)
                    .WithMany(p => p.Shapes)
                    .UsingEntity<Dictionary<string, object>>(
                        "GoodShape",
                        l => l.HasOne<Element>().WithMany().HasForeignKey("Element").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GoodShape__Eleme__3F466844"),
                        r => r.HasOne<Shape>().WithMany().HasForeignKey("Shape").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GoodShape__Shape__3E52440B"),
                        j =>
                        {
                            j.HasKey("Shape", "Element").HasName("PK__GoodShap__7925F4D569E60FF8");

                            j.ToTable("GoodShape");

                            j.IndexerProperty<string>("Shape").HasMaxLength(50);

                            j.IndexerProperty<string>("Element").HasMaxLength(50);
                        });
            });

            modelBuilder.Entity<TypeColor>(entity =>
            {
                entity.HasKey(e => new { e.KoiType, e.Color })
                    .HasName("PK__Type_Col__E7C4841F9BDCB48C");

                entity.ToTable("Type_Color");

                entity.Property(e => e.KoiType).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.HasOne(d => d.ColorNavigation)
                    .WithMany(p => p.TypeColors)
                    .HasForeignKey(d => d.Color)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Type_Colo__Color__4D94879B");

                entity.HasOne(d => d.KoiTypeNavigation)
                    .WithMany(p => p.TypeColors)
                    .HasForeignKey(d => d.KoiType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Type_Colo__KoiTy__4CA06362");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
