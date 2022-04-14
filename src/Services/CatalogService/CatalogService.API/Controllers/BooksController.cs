using CatalogService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var booksMock = new List<Book>
            {
                new Book  {Name = "Book 1"},
                new Book  {Name = "Book 2"},
                new Book  {Name = "Book 3"}
            };

            return Ok(booksMock);
        }
    }
}
