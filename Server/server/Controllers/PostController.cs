using System.Net.Http;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using server.Model;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]

public class PostController : ApiController
{
    //private readonly ILogger<PostController> _logger;

    public PostController()//ILogger<PostController> logger)
    {
        //_logger = logger;
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