using Model;
namespace Database;


public class Context : DbContext
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<Post> Posts => Set<Post>();

    public DbSet<User> Users => Set<User>();
    public DbSet<Supervisor> Supervisors => Set<Supervisor>();

    public Context(DbContextOptions<Context> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
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
    }
}
