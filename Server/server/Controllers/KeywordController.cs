using System.Reflection.Emit;
using System.Reflection;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using server.Model;
using server.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeywordController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IKeywordRepository _repository;

    public KeywordController(ILogger<PostController> logger, IKeywordRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [EnableCors]
    [HttpGet]
    public IEnumerable<KeywordDTO> Get()
    {
        return Enumerable.Range(1, 1).Select(index => new KeywordDTO(1, "Random word"))
        .ToArray();
    }

    [EnableCors]
    [HttpPost]
    public KeywordDTO Post(KeywordDTO keywordDTO)
    {
        return new KeywordDTO( 2,"Random word 2");

    }
}