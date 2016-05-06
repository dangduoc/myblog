namespace MyFirstBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyBlogDBModel : DbContext
    {
        public MyBlogDBModel()
            : base("name=MyBlogDBModel")
        {
        }

        public virtual DbSet<BestMoment> BestMoments { get; set; }
        public virtual DbSet<DailyPost> DailyPosts { get; set; }
        public virtual DbSet<imageDailyPost> imageDailyPosts { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<userInfo> userInfoes { get; set; }
        public virtual DbSet<userRole> userRoles { get; set; }
        public virtual DbSet<previewTrip> previewTrips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyPost>()
                .HasMany(e => e.imageDailyPosts)
                .WithRequired(e => e.DailyPost)
                .HasForeignKey(e => e.postID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyPost>()
                .HasMany(e => e.imageDailyPosts1)
                .WithRequired(e => e.DailyPost1)
                .HasForeignKey(e => e.postID);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.useremail)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasOptional(e => e.userInfo)
                .WithRequired(e => e.user);

            modelBuilder.Entity<userInfo>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<userRole>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<userRole>()
                .HasMany(e => e.users)
                .WithOptional(e => e.userRole)
                .HasForeignKey(e => e.role_id);
        }
    }
}
