using Microsoft.AspNetCore.Mvc;
using BookStore.Core;
using BookStore.Core.Model;
using BookStore.WebApi.Requests;


namespace BookStore.WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthorController : ControllerBase
    {

        private readonly AuthorService authorService;

        public AuthorController(AuthorService authorservice)
        {
            this.authorService = authorservice;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            try
            {
                Author author = await authorService.GetAuthor(id);
                return Ok(author);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Add")]

        public async Task<IActionResult> AddAuthor(Author author)
        {
            try
            {
                await authorService.AddAuthor(author.Name);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Update")]

        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest author)
        {
            try
            {
                await authorService.UpdateAuthor(author.Id, author.Name);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("Delete")]

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await authorService.DeleteAuthor(id);

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }



    }
}
