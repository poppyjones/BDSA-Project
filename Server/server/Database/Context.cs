using Model;

public class Context : DbContext
{
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
                    .HasMany<Supervisor>(p => p.CollaboratingUsers)
                    .WithMany(s => s.CollaboratingPosts);

        modelBuilder.Entity<Supervisor>()
                    .HasMany<Post>(s => s.OwnedPosts)
                    .WithOne(p => p.Author);
                    
    }
}
