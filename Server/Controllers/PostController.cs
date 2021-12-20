using System.Net.Http;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using server.Model;
using server.Repositories;
using server.Interfaces;


namespace server.Controllers
{
    
    [ApiController]
    [Route("[controller]")]

    public class PostController : ApiController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _repository;

        public PostController(ILogger<PostController> logger, IPostRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<PostDTO> Get()
        {
            return _repository.ReadAllByAuthorId(1);
        }

        [HttpPost]
        public int Post(PostDTO postDTO)
        {
            var dto = new PostCreateDTO(
                postDTO.Title,
                postDTO.AuthorId,
                postDTO.Created,
                postDTO.Ended,
                postDTO.Status,
                postDTO.Description,
                postDTO.Keywords
            );
            return _repository.Create(dto);
        }
    }
}