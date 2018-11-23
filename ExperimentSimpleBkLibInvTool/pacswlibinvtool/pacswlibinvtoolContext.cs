using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class pacswlibinvtoolContext : DbContext
    {
        public pacswlibinvtoolContext()
        {
        }

        public pacswlibinvtoolContext(DbContextOptions<pacswlibinvtoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authorstab> Authorstab { get; set; }
        public virtual DbSet<Bkconditions> Bkconditions { get; set; }
        public virtual DbSet<Bkstatuses> Bkstatuses { get; set; }
        public virtual DbSet<Bksynopsis> Bksynopsis { get; set; }
        public virtual DbSet<Bookcategories> Bookcategories { get; set; }
        public virtual DbSet<Bookcondition> Bookcondition { get; set; }
        public virtual DbSet<Bookformat> Bookformat { get; set; }
        public virtual DbSet<Bookinfo> Bookinfo { get; set; }
        public virtual DbSet<Forsale> Forsale { get; set; }
        public virtual DbSet<Owned> Owned { get; set; }
        public virtual DbSet<Publishinginfo> Publishinginfo { get; set; }
        public virtual DbSet<Purchaseinfo> Purchaseinfo { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<Volumeinseries> Volumeinseries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string DbConnectionStr = ConfigurationManager.ConnectionStrings["LibInvToolDBConnStr"].ConnectionString;
                optionsBuilder.UseMySQL(DbConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authorstab>(entity =>
            {
                entity.HasKey(e => new { e.IdAuthors, e.LastName, e.FirstName });

                entity.ToTable("authorstab", "pacswlibinvtool");

                entity.HasIndex(e => e.IdAuthors)
                    .HasName("idAuthors_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.LastName)
                    .HasName("LastName");

                entity.HasIndex(e => new { e.LastName, e.FirstName })
                    .HasName("LastCMFirst");

                entity.Property(e => e.IdAuthors)
                    .HasColumnName("idAuthors")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.YearOfBirth)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.YearOfDeath)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bkconditions>(entity =>
            {
                entity.HasKey(e => e.IdBkConditions);

                entity.ToTable("bkconditions", "pacswlibinvtool");

                entity.HasIndex(e => e.ConditionOfBookStr)
                    .HasName("ConditionOfBook_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdBkConditions)
                    .HasName("idbkconditions_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdBkConditions)
                    .HasColumnName("idBkConditions")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ConditionOfBookStr)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bkstatuses>(entity =>
            {
                entity.HasKey(e => e.IdBkStatus);

                entity.ToTable("bkstatuses", "pacswlibinvtool");

                entity.HasIndex(e => e.BkStatusStr)
                    .HasName("BkStatusStr_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdBkStatus)
                    .HasName("idbkstatus_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdBkStatus)
                    .HasColumnName("idBkStatus")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BkStatusStr)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bksynopsis>(entity =>
            {
                entity.HasKey(e => e.BookFksyop);

                entity.ToTable("bksynopsis", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFksyop)
                    .HasName("BookFKsYnop");

                entity.Property(e => e.BookFksyop)
                    .HasColumnName("BookFKsyop")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.StoryLine)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookFksyopNavigation)
                    .WithOne(p => p.Bksynopsis)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Bksynopsis>(d => d.BookFksyop)
                    .HasConstraintName("BookInfoFKSynopsis");
            });

            modelBuilder.Entity<Bookcategories>(entity =>
            {
                entity.HasKey(e => new { e.IdBookCategories, e.CategoryName });

                entity.ToTable("bookcategories", "pacswlibinvtool");

                entity.HasIndex(e => e.CategoryName)
                    .HasName("CategoryNames");

                entity.HasIndex(e => e.IdBookCategories)
                    .HasName("idBookCategories_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdBookCategories)
                    .HasColumnName("idBookCategories")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bookcondition>(entity =>
            {
                entity.HasKey(e => e.BookFkcond);

                entity.ToTable("bookcondition", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFkcond)
                    .HasName("BookFKCond_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.BookHasBeenRead)
                    .HasName("IsReadIndex");

                entity.HasIndex(e => e.ConditionOfBook)
                    .HasName("conditionindexfk_idx");

                entity.HasIndex(e => e.IsSignedByAuthor)
                    .HasName("IsSignedIndex");

                entity.HasIndex(e => e.NewOrUsed)
                    .HasName("statusindexfk_idx");

                entity.Property(e => e.BookFkcond)
                    .HasColumnName("BookFKCond")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.BookHasBeenRead).HasColumnType("tinyint(4)");

                entity.Property(e => e.ConditionOfBook).HasColumnType("int(10) unsigned");

                entity.Property(e => e.IsSignedByAuthor).HasColumnType("tinyint(4)");

                entity.Property(e => e.NewOrUsed).HasColumnType("int(10) unsigned");

                entity.Property(e => e.PhysicalDescriptionStr)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookFkcondNavigation)
                    .WithOne(p => p.Bookcondition)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Bookcondition>(d => d.BookFkcond)
                    .HasConstraintName("condbookinfoidxfk");

                entity.HasOne(d => d.ConditionOfBookNavigation)
                    .WithMany(p => p.Bookcondition)
                    .HasForeignKey(d => d.ConditionOfBook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("conditionindexfk");

                entity.HasOne(d => d.NewOrUsedNavigation)
                    .WithMany(p => p.Bookcondition)
                    .HasForeignKey(d => d.NewOrUsed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statusindexfk");
            });

            modelBuilder.Entity<Bookformat>(entity =>
            {
                entity.HasKey(e => new { e.IdFormat, e.FormatName });

                entity.ToTable("bookformat", "pacswlibinvtool");

                entity.HasIndex(e => e.FormatName)
                    .HasName("FormatName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdFormat)
                    .HasName("idFormat_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdFormat)
                    .HasColumnName("idFormat")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FormatName)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bookinfo>(entity =>
            {
                entity.HasKey(e => new { e.IdBookInfo, e.TitleFkbi, e.AuthorFkbi });

                entity.ToTable("bookinfo", "pacswlibinvtool");

                entity.HasIndex(e => e.AuthorFkbi)
                    .HasName("AuthorFKbi");

                entity.HasIndex(e => e.BookFormatFkbi)
                    .HasName("BookFormatFKBi");

                entity.HasIndex(e => e.CategoryFkbi)
                    .HasName("CategoryFKbI");

                entity.HasIndex(e => e.IdBookInfo)
                    .HasName("idBookInfo_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SeriesFkbi)
                    .HasName("SeriesFKBi");

                entity.HasIndex(e => e.TitleFkbi)
                    .HasName("TitleFKbi");

                entity.Property(e => e.IdBookInfo)
                    .HasColumnName("idBookInfo")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TitleFkbi)
                    .HasColumnName("TitleFKbi")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AuthorFkbi)
                    .HasColumnName("AuthorFKbi")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BookFormatFkbi)
                    .HasColumnName("BookFormatFKbi")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CategoryFkbi)
                    .HasColumnName("CategoryFKbi")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SeriesFkbi)
                    .HasColumnName("SeriesFKBi")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.AuthorFkbiNavigation)
                    .WithMany(p => p.Bookinfo)
                    .HasPrincipalKey(p => p.IdAuthors)
                    .HasForeignKey(d => d.AuthorFkbi)
                    .HasConstraintName("BkAuthorBookFK");

                entity.HasOne(d => d.BookFormatFkbiNavigation)
                    .WithMany(p => p.Bookinfo)
                    .HasPrincipalKey(p => p.IdFormat)
                    .HasForeignKey(d => d.BookFormatFkbi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BkFormatBookFK");

                entity.HasOne(d => d.CategoryFkbiNavigation)
                    .WithMany(p => p.Bookinfo)
                    .HasPrincipalKey(p => p.IdBookCategories)
                    .HasForeignKey(d => d.CategoryFkbi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BkCatBookFK");

                entity.HasOne(d => d.TitleFkbiNavigation)
                    .WithMany(p => p.Bookinfo)
                    .HasPrincipalKey(p => p.IdTitle)
                    .HasForeignKey(d => d.TitleFkbi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TitleBookFK");
            });

            modelBuilder.Entity<Forsale>(entity =>
            {
                entity.HasKey(e => e.BookFkfs);

                entity.ToTable("forsale", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFkfs)
                    .HasName("BookFKfs");

                entity.Property(e => e.BookFkfs)
                    .HasColumnName("BookFKfs")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.AskingPrice).HasDefaultValueSql("0");

                entity.Property(e => e.EstimatedValue).HasDefaultValueSql("0");

                entity.Property(e => e.IsForSale)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.BookFkfsNavigation)
                    .WithOne(p => p.Forsale)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Forsale>(d => d.BookFkfs)
                    .HasConstraintName("fsBookFK");
            });

            modelBuilder.Entity<Owned>(entity =>
            {
                entity.HasKey(e => e.BookFko);

                entity.ToTable("owned", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFko)
                    .HasName("BookFKo");

                entity.HasIndex(e => e.IsOwned)
                    .HasName("ownedindex");

                entity.HasIndex(e => e.IsWishListed)
                    .HasName("wishindex");

                entity.Property(e => e.BookFko)
                    .HasColumnName("BookFKo")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsOwned).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsWishListed).HasColumnType("tinyint(4)");

                entity.HasOne(d => d.BookFkoNavigation)
                    .WithOne(p => p.Owned)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Owned>(d => d.BookFko)
                    .HasConstraintName("ownedBookFK");
            });

            modelBuilder.Entity<Publishinginfo>(entity =>
            {
                entity.HasKey(e => e.BookFkpubI);

                entity.ToTable("publishinginfo", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFkpubI)
                    .HasName("BookFKPubI");

                entity.HasIndex(e => e.Isbnumber)
                    .HasName("ISBNindex");

                entity.Property(e => e.BookFkpubI)
                    .HasColumnName("BookFKPubI")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Copyright)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Edition).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Isbnumber)
                    .HasColumnName("ISBNumber")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.OutOfPrint).HasColumnType("tinyint(4)");

                entity.Property(e => e.Printing).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookFkpubINavigation)
                    .WithOne(p => p.Publishinginfo)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Publishinginfo>(d => d.BookFkpubI)
                    .HasConstraintName("bookDataFKpub");
            });

            modelBuilder.Entity<Purchaseinfo>(entity =>
            {
                entity.HasKey(e => e.BookFkpurI);

                entity.ToTable("purchaseinfo", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFkpurI)
                    .HasName("BookFKPurI");

                entity.HasIndex(e => e.PurchaseDate)
                    .HasName("DateBoughtIndex");

                entity.Property(e => e.BookFkpurI)
                    .HasColumnName("BookFKPurI")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookFkpurINavigation)
                    .WithOne(p => p.Purchaseinfo)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Purchaseinfo>(d => d.BookFkpurI)
                    .HasConstraintName("purBookFK");
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.HasKey(e => e.BookFkrats);

                entity.ToTable("ratings", "pacswlibinvtool");

                entity.HasIndex(e => e.AmazonRatings)
                    .HasName("AmazonRatings_idx");

                entity.HasIndex(e => e.BookFkrats)
                    .HasName("BookFKRats_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.GoodReadsRatings)
                    .HasName("GoodReadsRats_idx");

                entity.HasIndex(e => e.MyRatings)
                    .HasName("MyRatings_idx");

                entity.Property(e => e.BookFkrats)
                    .HasColumnName("BookFKRats")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.BookFkratsNavigation)
                    .WithOne(p => p.Ratings)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Ratings>(d => d.BookFkrats)
                    .HasConstraintName("BookFKRats");
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasKey(e => new { e.IdSeries, e.AuthorOfSeries, e.SeriesName });

                entity.ToTable("series", "pacswlibinvtool");

                entity.HasIndex(e => e.AuthorOfSeries)
                    .HasName("AuthorFKs");

                entity.HasIndex(e => e.IdSeries)
                    .HasName("idSeries_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SeriesName)
                    .HasName("SeriesTitle");

                entity.Property(e => e.IdSeries)
                    .HasColumnName("idSeries")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AuthorOfSeries).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SeriesName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorOfSeriesNavigation)
                    .WithMany(p => p.Series)
                    .HasPrincipalKey(p => p.IdAuthors)
                    .HasForeignKey(d => d.AuthorOfSeries)
                    .HasConstraintName("authorfksidx");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => new { e.IdTitle, e.TitleStr });

                entity.ToTable("title", "pacswlibinvtool");

                entity.HasIndex(e => e.IdTitle)
                    .HasName("idTitle_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TitleStr)
                    .HasName("TitleStr");

                entity.Property(e => e.IdTitle)
                    .HasColumnName("idTitle")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TitleStr)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Volumeinseries>(entity =>
            {
                entity.HasKey(e => e.BookFkvs);

                entity.ToTable("volumeinseries", "pacswlibinvtool");

                entity.HasIndex(e => e.BookFkvs)
                    .HasName("BookFKvsidx");

                entity.HasIndex(e => e.SeriesFk)
                    .HasName("SeriesFKvsidx");

                entity.Property(e => e.BookFkvs)
                    .HasColumnName("BookFKvs")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SeriesFk)
                    .HasColumnName("SeriesFK")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VolumeNumber).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.BookFkvsNavigation)
                    .WithOne(p => p.Volumeinseries)
                    .HasPrincipalKey<Bookinfo>(p => p.IdBookInfo)
                    .HasForeignKey<Volumeinseries>(d => d.BookFkvs)
                    .HasConstraintName("BookInfoFKvolumeS");

                entity.HasOne(d => d.SeriesFkNavigation)
                    .WithMany(p => p.Volumeinseries)
                    .HasPrincipalKey(p => p.IdSeries)
                    .HasForeignKey(d => d.SeriesFk)
                    .HasConstraintName("SeriesFKVolumeS");
            });
        }
    }
}
