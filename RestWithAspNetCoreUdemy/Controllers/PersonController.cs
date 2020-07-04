        using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
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
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }


        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person =_personService.FindById(id);
            if (person == null) return NotFound();
            
            return Ok(person);
        }


        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]PersonVO person)
        {
            if (person == null) return NotFound();
            return Ok(_personService.Create(person));
        }


        [HttpPut("{id}")]
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

        
        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
