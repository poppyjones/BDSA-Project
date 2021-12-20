using server.Database;
using server.Model;

namespace server.Interfaces;

public interface IKeywordRepository
{
    int Create(KeywordDTO keyword);
}

