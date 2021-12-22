using System.Diagnostics.Tracing;
using System.ComponentModel.Design;
using server.Interfaces;
using server.Database;
using server.Model;
namespace server.Repositories
{
    public class KeywordRepository : IKeywordRepository, IDisposable
    {
        private readonly IContext _context;

        public KeywordRepository(IContext context)
        {
            _context = context;
        }

        public int Create(KeywordCreateDTO keywordCreateDTO)
        {
            //checks if the keyword already exists
            var keyword = from k in _context.Keywords
                          where k.Name == keywordCreateDTO.Name
                          select new KeywordDTO(
                              k.Id,
                              k.Name
                          );
            if (keyword.FirstOrDefault() is not null) return keyword.FirstOrDefault().Id;

            var newKeyword = new Keyword { Name = keywordCreateDTO.Name };

            _context.Keywords.Add(newKeyword);
            _context.SaveChanges();

            return newKeyword.Id;
        }
        public ICollection<KeywordDTO> ReadAllKeywords()
        {
            var keywords = from k in _context.Keywords
                           select new KeywordDTO(
                               k.Id,
                               k.Name
                           );
            return keywords.ToList();
        }

        public KeywordDTO ReadById(int keywordId)
        {
            var keyword = from k in _context.Keywords
                          where k.Id == keywordId
                          select new KeywordDTO(
                              k.Id,
                              k.Name
                          );
            return keyword.FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}