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

public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _repository;

    public PostController(ILogger<PostController> logger, IPostRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [EnableCors]
    [HttpGet]
    public IEnumerable<PostDTO> Get()
    {
        throw new NotImplementedException();
        // return Enumerable.Range(1, 1).Select(index => new PostDTO
        // {
        //     Id = 1,
        //     Topic = "TestTopic",
        //     Description = "Test Description",
        //     Keywords = "Random Keyword"
        // })
        // .ToArray();
    }

    [EnableCors]
    [HttpPost]
    public PostDTO Post(PostDTO postDTO)
    {
        throw new NotImplementedException();
        // return new PostDTO
        // {
        //     Id = 2,
        //     Topic = "TestTopic2",
        //     Description = "Test Description 2",
        //     Keywords = "Random Keyword 2",
        // };

    }
}