using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using json_resume.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace json_resume.Controllers
{
    [ApiController]
    [Route("resume/[controller]")]
    public class VolunteersController : ControllerBase
    {
        private readonly ILogger<VolunteersController> _logger;
        private Resume _resume;

        public VolunteersController(ILogger<VolunteersController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Volunteer>> Get()
        {
            var res = _resume.volunteers;
            return Ok(res);
        }

        [HttpGet("{organization}")]
        public ActionResult<List<Volunteer>> Get([FromRoute] string organization)
        {
            if(organization == null || organization == "")
                return BadRequest();

            var res = _resume.volunteers.FirstOrDefault(p => p.organization == organization);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Volunteer> Create([FromBody] Volunteer req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.volunteers.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{organization}")]
        public ActionResult<Volunteer> Delete([FromRoute] string organization)
        {
            if(organization == null || organization == "")
                return BadRequest();

            var res = _resume.volunteers.FirstOrDefault(p => p.organization == organization);

            if(res == null){
                return NotFound();
            }

            _resume.volunteers.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Volunteer> Update([FromBody] Volunteer req)
        {
            if(req.organization == null || req.organization == "")
            {
                return BadRequest();
            }

            var Volunteer = _resume.volunteers.FirstOrDefault( p => p.organization == req.organization);

            if(Volunteer != null){
                _resume.volunteers.Remove(Volunteer);
            }
            else{
                return NotFound();
            }

            _resume.volunteers.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
                
        [HttpPatch("{organization}")]
        public ActionResult<Volunteer> PartialUpdate([FromRoute] string organization, 
        [FromBody] JsonPatchDocument<Volunteer> patchDocument)
        {
            if(organization == null  || organization == "" || patchDocument == null)
                return BadRequest();

            var obj = _resume.volunteers.FirstOrDefault(o => o.organization == organization);
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }


    }
}
