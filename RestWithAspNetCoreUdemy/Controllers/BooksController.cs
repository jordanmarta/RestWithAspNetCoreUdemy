        using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Services.Interfaces;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class BooksController : Controller
    {
        private IBookService _bookService;

        public BooksController(IBookService personService)
        {
            _bookService = personService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.FindById(id);
            if (book == null) return NotFound();

            return Ok(book);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (book == null) return NotFound();
                return Ok(_bookService.Create(book));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Book book)
        {
            if (book == null)
                return BadRequest();

            var updatedBook = _bookService.Update(book);

            if (updatedBook == null)
                return NoContent();

            return new ObjectResult(book);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return NoContent();
        }
    }
}
