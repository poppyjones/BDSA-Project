
using Model;
public class Context : DbContext, IContext
{
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Attribute>();
        modelBuilder.Entity<Post>().HasOne<User>(p => p.Author).WithMany(s => s.OwnedPosts);
        modelBuilder.Entity<Post>().HasMany<User>(p => p.CollaboratingUsers).WithMany(u => u.CollaboratingPosts);
        modelBuilder.Entity<User>().HasMany<Post>(u => u.CollaboratingPosts).WithMany(p => p.CollaboratingUsers);
    }
}
