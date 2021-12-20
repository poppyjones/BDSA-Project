using server.Interfaces;
using server.Database;
using server.Model;

namespace server.Repositories;

public class KeywordRepository : IKeywordRepository
{

    private readonly IContext _context;

    public KeywordRepository(IContext context)
    {
        _context = context;
    }

    public int Create(KeywordDTO keyword)
    {
        var newKeyword = new Keyword{
            Id = keyword.Id,
            Name = keyword.Name

        };
    _context.Keywords.Add(newKeyword);
    _context.SaveChanges();

    return keyword.Id;
    }
    
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
