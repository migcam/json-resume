using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using json_resume.Models;

namespace json_resume.Controllers
{
    [ApiController]
    [Route("resume/[controller]")]
    public class PublicationsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private Resume _resume;

        public PublicationsController(ILogger<PublicationsController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Publication>> Get()
        {
            var res = _resume.publications;
            return Ok(res);
        }

        [HttpGet("{name}")]
        public ActionResult<List<Publication>> Get([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.publications.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Publication> Create([FromBody] Publication req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.publications.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{name}")]
        public ActionResult<Publication> Delete([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.publications.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            _resume.publications.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Publication> Update([FromBody] Publication req)
        {
            if(req.name == null || req.name == "")
            {
                return BadRequest();
            }

            var Publication = _resume.publications.FirstOrDefault( p => p.name == req.name);

            if(Publication != null){
                _resume.publications.Remove(Publication);
            }
            else{
                return NotFound();
            }

            _resume.publications.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
    }
}
