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
        
    }
}
