using server.Model;

namespace server.Database;
public class Context : DbContext, IContext
{
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Attribute>();

        modelBuilder.Entity<Post>().HasMany<User>(p => p.Users).WithMany(u => u.Posts);
        modelBuilder.Entity<User>().HasMany<Post>(u => u.Posts).WithMany(p => p.Users);

        modelBuilder.Entity<Post>().HasMany<Keyword>(p => p.Keywords).WithMany(k => k.Posts);
        modelBuilder.Entity<Keyword>().HasMany<Post>(k => k.Posts).WithMany(p => p.Keywords);

        base.OnModelCreating(modelBuilder);
    }
}
