using Safit.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Safit.Core.DataAccess;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartContent> CartContents { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseAccess> CourseAccesses { get; set; }

    public virtual DbSet<FetchSource> FetchSources { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    public virtual DbSet<Specialisation> Specialisations { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart__3213E83FB2EEDDBC");

            entity.ToTable("cart", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart__user_id__619B8048");
        });

        modelBuilder.Entity<CartContent>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.ProductId }).HasName("PK__cart_con__6A850DF8EAA5517F");

            entity.ToTable("cart_content", "sf");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartContents)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_cont__cart___656C112C");

            entity.HasOne(d => d.Product).WithMany(p => p.CartContents)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_cont__produ__66603565");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83F20EF582B");

            entity.ToTable("comment", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.InverseBranch)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__comment__branch___5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__user_id__5CD6CB2B");

            entity.HasOne(d => d.Video).WithMany(p => p.Comments)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__video_i__5DCAEF64");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__course__3213E83F1021EC1A");

            entity.ToTable("course", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Specialisation).WithMany(p => p.Courses)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__48CFD27E");
        });

        modelBuilder.Entity<CourseAccess>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CourseId }).HasName("PK__course_a__414FD8754FC91981");

            entity.ToTable("course_access", "sf");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.PurchaseDt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("purchase_dt");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseAccesses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course_ac__cours__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.CourseAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course_ac__user___6A30C649");
        });

        modelBuilder.Entity<FetchSource>(entity =>
        {
            entity.HasKey(e => new { e.VideoId, e.Source }).HasName("PK__fetch_so__C83FBB548AED90A6");

            entity.ToTable("fetch_source", "sf");

            entity.Property(e => e.VideoId).HasColumnName("video_id");
            entity.Property(e => e.Source)
                .HasMaxLength(65)
                .IsUnicode(false)
                .HasColumnName("source");

            entity.HasOne(d => d.Video).WithMany(p => p.FetchSources)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__fetch_sou__video__797309D9");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__post__3213E83FC9FBFD54");

            entity.ToTable("post", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(2500)
                .HasColumnName("content");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.Views).HasColumnName("views");

            entity.HasOne(d => d.Course).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__post__course_id__4D94879B");

            entity.HasOne(d => d.Specialisation).WithMany(p => p.Posts)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__post__4CA06362");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__post_tag__tag_id__503BEA1C"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__post_tag__post_i__4F47C5E3"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("PK__post_tag__4AFEED4D6672BEFA");
                        j.ToTable("post_tag", "sf");
                        j.IndexerProperty<long>("PostId").HasColumnName("post_id");
                        j.IndexerProperty<long>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83FC07E423C");

            entity.ToTable("product", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.Link)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Specialisation).WithMany(p => p.Products)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__5629CD9C");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TagId }).HasName("PK__recommen__CD975D2491D30354");

            entity.ToTable("recommendation", "sf");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Tag).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recommend__tag_i__76969D2E");

            entity.HasOne(d => d.User).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recommend__user___75A278F5");
        });

        modelBuilder.Entity<Specialisation>(entity =>
        {
            entity.HasKey(e => new { e.TrainerId, e.SportId }).HasName("PK__speciali__65E7244D13D0040A");

            entity.ToTable("specialisation", "sf");

            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");

            entity.HasOne(d => d.Sport).WithMany(p => p.Specialisations)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__specialis__sport__45F365D3");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Specialisations)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__specialis__train__44FF419A");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sport__3213E83FB04467D6");

            entity.ToTable("sport", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.PreviewSrc)
                .HasMaxLength(65)
                .IsUnicode(false)
                .HasColumnName("preview_src");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tag__3213E83F9DCEB63E");

            entity.ToTable("tag", "sf");

            entity.HasIndex(e => e.Name, "UQ__tag__72E12F1BB59E5E12").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(65)
                .HasColumnName("name");

            entity.HasMany(d => d.Videos).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "VideoTag",
                    r => r.HasOne<Video>().WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__video_tag__video__71D1E811"),
                    l => l.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__video_tag__tag_i__70DDC3D8"),
                    j =>
                    {
                        j.HasKey("TagId", "VideoId").HasName("PK__video_ta__5C19B357944324E6");
                        j.ToTable("video_tag", "sf");
                        j.IndexerProperty<long>("TagId").HasColumnName("tag_id");
                        j.IndexerProperty<long>("VideoId").HasColumnName("video_id");
                    });
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trainer__3213E83F18071F45");

            entity.ToTable("trainer", "sf");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Trainer)
                .HasForeignKey<Trainer>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trainer__id__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83FE9277DE4");

            entity.ToTable("user", "sf");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E61649067D499").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__user__F3DBC57278BA90D3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(32)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(32)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasColumnName("password");
            entity.Property(e => e.ProfileSrc)
                .HasMaxLength(65)
                .IsUnicode(false)
                .HasColumnName("profile_src");
            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .HasColumnName("username");

            entity.HasMany(d => d.Trainers).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Subscription",
                    r => r.HasOne<Trainer>().WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__subscript__train__403A8C7D"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__subscript__user___3F466844"),
                    j =>
                    {
                        j.HasKey("UserId", "TrainerId").HasName("PK__subscrip__3FE47C6DFAE3D90C");
                        j.ToTable("subscription", "sf");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("TrainerId").HasColumnName("trainer_id");
                    });

            entity.HasMany(d => d.Videos).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Like",
                    r => r.HasOne<Video>().WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__like__video_id__59FA5E80"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__like__user_id__59063A47"),
                    j =>
                    {
                        j.HasKey("UserId", "VideoId").HasName("PK__like__A73126EEFF3CFFB1");
                        j.ToTable("like", "sf");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("VideoId").HasColumnName("video_id");
                    });
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__video__3213E83FD9216D31");

            entity.ToTable("video", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.Views).HasColumnName("views");
            entity.Property(e => e.Visible).HasColumnName("visible");

            entity.HasOne(d => d.Course).WithMany(p => p.Videos)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__video__course_id__534D60F1");

            entity.HasOne(d => d.Specialisation).WithMany(p => p.Videos)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__video__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
