using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using server.Model;
using server.Interfaces;

namespace server.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class KeywordController : ApiController
    {
        private readonly ILogger<KeywordController> _logger;

        private readonly IKeywordRepository _repository;

        public KeywordController(ILogger<KeywordController> logger, IKeywordRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<KeywordDTO> Get()
        {
            return _repository.ReadAllKeywords();
        }

        [HttpPost]
        public int Post(KeywordCreateDTO keywordDTO)
        {
            var dto = new KeywordCreateDTO(
                keywordDTO.Name
            );
            return _repository.Create(dto);
        }

    }
}
