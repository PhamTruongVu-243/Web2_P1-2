using BT1.Data;
using BT1.Models.DTO;
using BT1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BT1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet("get-all-author")]
        public IActionResult GetAllAuthor()
        {
            var allAuthors = _authorRepository.GetAllAuthors();
            return Ok(allAuthors);
        }

        [HttpGet]
        [Route("get-author-by-id/{id}")]
        public IActionResult GetAuthorById([FromRoute] int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorAdd = _authorRepository.AddAuthor(addAuthorRequestDTO);
            return Ok(authorAdd);
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorNoIdDTO authorNoIdDTO)
        {
            var updateAuthor = _authorRepository.UpdateAuthorById(id, authorNoIdDTO);
            if (updateAuthor == null)
            {
                return NotFound();
            }
            return Ok(updateAuthor);
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            var deleteAuthor = _authorRepository.DeleteAuthorById(id);
            if (deleteAuthor == null)
            {
                return NotFound();
            }
            return Ok(deleteAuthor);
        }
    }
}
