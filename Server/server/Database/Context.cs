using Model;

public class Context : DbContext
{
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Post> Posts { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // UserPost
        modelBuilder.Entity<UserPost>().HasKey(up => new { up.UserId, up.PostId });

        modelBuilder.Entity<UserPost>()
                    .HasOne<User>(u => u.User)
                    .WithMany(up => up.UserPost)
                    .HasForeignKey(u => u.UserId);


        modelBuilder.Entity<UserPost>()
                    .HasOne<Post>(p => p.Post)
                    .WithMany(up => up.UserPost)
                    .HasForeignKey(p => p.PostId);


        // PostKeyword
        modelBuilder.Entity<PostKeyword>().HasKey(pk => new { pk.PostId, pk.KeywordId });

        modelBuilder.Entity<PostKeyword>()
                    .HasOne<Post>(p => p.Post)
                    .WithMany(pk => pk.PostKeyword)
                    .HasForeignKey(p => p.PostId);


        modelBuilder.Entity<PostKeyword>()
                    .HasOne<Keyword>(k => k.Keyword)
                    .WithMany(pk => pk.PostKeyword)
                    .HasForeignKey(k => k.KeywordId);
    }
    
    
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasMany<Post>(u => u.collaborating_posts)
                    .WithMany(p => p.collaborators)
                    .Map(up =>
                            {
                                up.MapLeftKey("UserRefId");
                                up.MapRightKey("PostRefId");
                                up.ToTable("UserPost");
                            });
    }*/

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<UserPost>()
    //         .HasKey(up => new { up.PostId, up.TagId });

    //     modelBuilder.Entity<UserPost>()
    //         .HasOne(u => u.User)
    //         .WithMany(up => up.UserPost)
    //         .HasForeignKey(u => u.User);

    //     modelBuilder.Entity<UserPost>()
    //         .HasOne(p => p.Post)
    //         .WithMany(up => up.UserPost)
    //         .HasForeignKey(p => p.Post);
    // }
}
