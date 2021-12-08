namespace Model;

public interface IContext : IDisposable
{
    DbSet<Keyword> Keywords { get; }
    DbSet<Post> Posts { get; }
    DbSet<User> Users { get; }
    int SaveChanges();
}