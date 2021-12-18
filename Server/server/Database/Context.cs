using server.Model;

namespace server.Database;
public class Context : DbContext, IContext
{
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPost> UserPost { get; set; }
    public DbSet<KeywordPost> KeywordPost { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Attribute>();
        
        modelBuilder.Entity<Keyword>().HasMany<Post>(k => k.Posts).WithMany(p => p.Keywords);
        modelBuilder.Entity<Post>().HasMany<Keyword>(p => p.Keywords).WithMany(k => k.Posts);
        
        modelBuilder.Entity<Post>().HasMany<User>(p => p.Users).WithMany(u => u.Posts);
        modelBuilder.Entity<User>().HasMany<Post>(u => u.Posts).WithMany(p => p.Users);
        
        modelBuilder.Entity<KeywordPost>().HasKey(kp => new { kp.KeywordId, kp.PostId });
        modelBuilder.Entity<UserPost>().HasKey(pu => new { pu.UserId, pu.PostId});
        
        
        /* modelBuilder.Entity<Keyword>()
                .HasMany(k => k.Posts)
                .WithMany(k => k.Keywords)
                .UsingEntity<KeywordPost>(
                    j => j
                        .HasOne(kp => kp.Post)
                        .WithMany(p => p.KeywordPost)
                        .HasForeignKey(kp => kp.PostId),
                    j => j
                        .HasOne(kp => kp.Post)
                        .WithMany(k => k.KeywordPost)
                        .HasForeignKey(kp => kp.PostId),
                    j =>
                    {
                        j.HasKey(t => new { p.PostId, p.KeywordId });
                    }); */


        modelBuilder.Entity<Keyword>()
            .HasMany(p => p.Posts)
            .WithMany(p => p.Keywords)
            .UsingEntity<KeywordPost>(
                j => j
                    .HasOne(pt => pt.Post)
                    .WithMany(t => t.KeywordPost)
                    .HasForeignKey(pt => pt.PostId),
                j => j
                    .HasOne(pt => pt.Keyword)
                    .WithMany(p => p.KeywordPost)
                    .HasForeignKey(pt => pt.PostId),
                j =>
                {
                    j.HasKey(t => new { t.PostId, t.KeywordId });
                });


        modelBuilder.Entity<User>()
            .HasMany(p => p.Posts)
            .WithMany(p => p.Users)
            .UsingEntity<UserPost>(
                j => j
                    .HasOne(pt => pt.Post)
                    .WithMany(t => t.UserPost)
                    .HasForeignKey(pt => pt.PostId),
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(p => p.UserPost)
                    .HasForeignKey(pt => pt.PostId),
                j =>
                {
                    j.HasKey(t => new { t.PostId, t.UserId });
                });
    }

}
