using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Safit.Core.Domain.Models;

namespace Safit.Core.DataAccess.Context;

public partial class SafitDbContext : DbContext
{
    private IConfiguration configuration;

    public SafitDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public SafitDbContext(DbContextOptions<SafitDbContext> options, IConfiguration configuration)
        : base(options)
    {
        this.configuration = configuration;
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Timecode> Timecodes { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<TrainerSport> TrainerSports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoPart> VideoParts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(configuration["Safit:Database:ConnectionString"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("attachment", "sf");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Message).WithMany()
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__attachmen__messa__3C69FB99");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart__3213E83FC8DB479F");

            entity.ToTable("cart", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart__user_id__6754599E");

            entity.HasMany(d => d.Products).WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartContent",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__cart_cont__produ__6B24EA82"),
                    l => l.HasOne<Cart>().WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__cart_cont__cart___6A30C649"),
                    j =>
                    {
                        j.HasKey("CartId", "ProductId").HasName("PK__cart_con__6A850DF872B3FA1E");
                        j.ToTable("cart_content", "sf");
                        j.IndexerProperty<long>("CartId").HasColumnName("cart_id");
                        j.IndexerProperty<long>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83FC2D8C86F");

            entity.ToTable("comment", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.InverseBranch)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__comment__branch___6477ECF3");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__user_id__628FA481");

            entity.HasOne(d => d.Video).WithMany(p => p.Comments)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__video_i__6383C8BA");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__course__3213E83F40AE53FA");

            entity.ToTable("course", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.TrainerSport).WithMany(p => p.Courses)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__4BAC3F29");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message__3213E83FD3C629F4");

            entity.ToTable("message", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DestUserId).HasColumnName("dest_user_id");
            entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

            entity.HasOne(d => d.DestUser).WithMany(p => p.MessageDestUsers)
                .HasForeignKey(d => d.DestUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message__dest_us__3A81B327");

            entity.HasOne(d => d.FromUser).WithMany(p => p.MessageFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message__from_us__398D8EEE");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__post__3213E83FB1CD451E");

            entity.ToTable("post", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__post__course_id__4F7CD00D");

            entity.HasOne(d => d.TrainerSport).WithMany(p => p.Posts)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__post__4E88ABD4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F6EDF4677");

            entity.ToTable("product", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.TrainerSport).WithMany(p => p.Products)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__5629CD9C");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sport__3213E83F2BB31EDE");

            entity.ToTable("sport", "sf");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Timecode>(entity =>
        {
            entity.HasKey(e => new { e.VideoId, e.Timing }).HasName("PK__timecode__B1842B4C8FE9AB40");

            entity.ToTable("timecode", "sf");

            entity.Property(e => e.VideoId).HasColumnName("video_id");
            entity.Property(e => e.Timing).HasColumnName("timing");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Timecodes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__timecode__course__59FA5E80");

            entity.HasOne(d => d.Post).WithMany(p => p.Timecodes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__timecode__post_i__5AEE82B9");

            entity.HasOne(d => d.Product).WithMany(p => p.Timecodes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__timecode__produc__59063A47");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Timecodes)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK__timecode__traine__5BE2A6F2");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trainer__3213E83F82E217FD");

            entity.ToTable("trainer", "sf");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Trainer)
                .HasForeignKey<Trainer>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trainer__id__3F466844");
        });

        modelBuilder.Entity<TrainerSport>(entity =>
        {
            entity.HasKey(e => new { e.TrainerId, e.SportId }).HasName("PK__trainer___65E7244DEF1FC3DC");

            entity.ToTable("trainer_sport", "sf");

            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");

            entity.HasOne(d => d.Sport).WithMany(p => p.TrainerSports)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trainer_s__sport__48CFD27E");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TrainerSports)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trainer_s__train__47DBAE45");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83FACB88BCA");

            entity.ToTable("user", "sf");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasMany(d => d.Courses).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseAccess",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__course_ac__cours__6EF57B66"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__course_ac__user___6E01572D"),
                    j =>
                    {
                        j.HasKey("UserId", "CourseId").HasName("PK__course_a__414FD8756948F98B");
                        j.ToTable("course_access", "sf");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("CourseId").HasColumnName("course_id");
                    });

            entity.HasMany(d => d.Trainers).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Subscription",
                    r => r.HasOne<Trainer>().WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__subscript__train__4316F928"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__subscript__user___4222D4EF"),
                    j =>
                    {
                        j.HasKey("UserId", "TrainerId").HasName("PK__subscrip__3FE47C6DE92761BF");
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
                        .HasConstraintName("FK__like__video_id__5FB337D6"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__like__user_id__5EBF139D"),
                    j =>
                    {
                        j.HasKey("UserId", "VideoId").HasName("PK__like__A73126EE5AA4D069");
                        j.ToTable("like", "sf");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("VideoId").HasColumnName("video_id");
                    });
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__video__3213E83F741BECE5");

            entity.ToTable("video", "sf");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Videos)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__video__course_id__534D60F1");

            entity.HasOne(d => d.TrainerSport).WithMany(p => p.Videos)
                .HasForeignKey(d => new { d.TrainerId, d.SportId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__video__52593CB8");
        });

        modelBuilder.Entity<VideoPart>(entity =>
        {
            entity.HasKey(e => new { e.PartId, e.VideoId }).HasName("PK__video_pa__BE6CEB594726011D");

            entity.ToTable("video_part", "sf");

            entity.Property(e => e.PartId)
                .ValueGeneratedOnAdd()
                .HasColumnName("part_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");
            entity.Property(e => e.Source)
                .HasMaxLength(65)
                .IsUnicode(false)
                .HasColumnName("source");

            entity.HasOne(d => d.Video).WithMany(p => p.VideoParts)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__video_par__video__71D1E811");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
