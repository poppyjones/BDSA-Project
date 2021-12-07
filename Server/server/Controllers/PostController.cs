using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers;

[ApiController]
[Route("[controller]")]

public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }

    [EnableCors]
    [HttpGet(Name = "GetPosts")]
    public IEnumerable<PostDTO> Get()
    {
        return Enumerable.Range(1, 1).Select(index => new PostDTO
        {
            Id = 1,
            Topic = "TestTopic",
            Description = "Test Description",
            Keywords = "Random Keyword"
        })
        .ToArray();
    }
}