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

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _repository;

    public UserController(ILogger<UserController> logger, IUserRepository repository)
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