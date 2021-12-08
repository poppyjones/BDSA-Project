namespace Model;

public interface IContext 
{
    DbSet<Keyword> Keywords { get; }
    DbSet<Post> Posts { get; }
    DbSet<User> Users { get; }
    int SaveChanges();
}