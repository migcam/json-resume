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
    public class InterestsController : ControllerBase
    {
        private readonly ILogger<InterestsController> _logger;
        private Resume _resume;

        public InterestsController(ILogger<InterestsController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Interest>> Get()
        {
            var res = _resume.interests;
            return Ok(res);
        }

        [HttpGet("{name}")]
        public ActionResult<List<Interest>> Get([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.interests.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Interest> Create([FromBody] Interest req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.interests.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{name}")]
        public ActionResult<Interest> Delete([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.interests.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            _resume.interests.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Interest> Update([FromBody] Interest req)
        {
            if(req.name == null || req.name == "")
            {
                return BadRequest();
            }

            var Interest = _resume.interests.FirstOrDefault( p => p.name == req.name);

            if(Interest != null){
                _resume.interests.Remove(Interest);
            }
            else{
                return NotFound();
            }

            _resume.interests.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
    }
}
