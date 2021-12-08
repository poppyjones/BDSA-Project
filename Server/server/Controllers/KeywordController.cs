using System.Net.Http;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]

public class KeywordController : ApiController
{
    //private readonly ILogger<PostController> _logger;

    public KeywordController()//ILogger<PostController> logger)
    {
        //_logger = logger;
    }

    [EnableCors]
    [HttpGet]
    public IEnumerable<KeywordDTO> Get()
    {
        return Enumerable.Range(1, 1).Select(index => new KeywordDTO
        {
            Id = 1,
            Name = "Random word"
        })
        .ToArray();
    }

    [EnableCors]
    [HttpPost]
    public KeywordDTO Post(KeywordDTO keywordDTO)
    {
        return new KeywordDTO
        {
            Id = 2,
            Name = "Random word 2"
        };

    }
}