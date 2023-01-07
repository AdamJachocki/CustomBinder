using Common.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddAuthor(IEnumerable<IFormFile> files, FormJsonData<Author> author)
        {
            return Ok();
        }

        [HttpPost("{post}")]
        public IActionResult AddPost(SimplePost post)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok();
        }

    }
}
