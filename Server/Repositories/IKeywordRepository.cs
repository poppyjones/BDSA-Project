using server.Database;
using server.Model;

namespace server.Interfaces;

public interface IKeywordRepository 
{
    int Create(KeywordCreateDTO keywordCreateDTO);
    ICollection<KeywordDTO> ReadAllKeywords();
    KeywordDTO ReadById(int keywordId);
}

