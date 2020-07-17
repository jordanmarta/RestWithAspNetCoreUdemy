using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Services.Interfaces;
using Tapioca.HATEOAS;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : Controller
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpGet]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("FindByName")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByName([FromQuery]string firstName, [FromQuery] string lastName)
        {
            return Ok(_personService.FindByName(firstName, lastName));
        }


        [HttpGet("GetPagedSearch/{sortDirection}/{pageSize}/{page}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetPagedSearch([FromQuery]string name, string sortDirection, int pageSize, int page)
        
        {
            return Ok(_personService.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }


        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person =_personService.FindById(id);
            if (person == null) return NotFound();
            
            return Ok(person);
        }


        [HttpPost]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]PersonVO person)
        {
            if (person == null) return NotFound();
            return Ok(_personService.Create(person));
        }


        [HttpPut("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]PersonVO person)
        {
            if (person == null) 
                return BadRequest();
            
            var updatedPerson = _personService.Update(person);
            
            if (updatedPerson == null)
                return NoContent();

            return new ObjectResult (updatedPerson);
        }


        [HttpPatch("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch([FromBody]PersonVO person)
        {
            if (person == null)
                return BadRequest();

            var updatedPerson = _personService.Update(person);

            if (updatedPerson == null)
                return NoContent();

            return new ObjectResult(updatedPerson);
        }


        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
