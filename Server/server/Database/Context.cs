namespace Ccontext;


public class Context : DbContext
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<Post> Posts => Set<Post>();
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Supervisor> Supervisor => Set<Supervisor>();
    
    public Context(DbContextOptions<Context> options) : base(options) { }
}
