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
    public class EducationsController : ControllerBase
    {
        private readonly ILogger<EducationsController> _logger;
        private Resume _resume;

        public EducationsController(ILogger<EducationsController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Education>> Get()
        {
            var res = _resume.educations;
            return Ok(res);
        }

        [HttpGet("{institution}")]
        public ActionResult<List<Education>> Get([FromRoute] string institution)
        {
            if(institution == null || institution == "")
                return BadRequest();

            var res = _resume.educations.FirstOrDefault(p => p.institution == institution);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Education> Create([FromBody] Education req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.educations.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{institution}")]
        public ActionResult<Education> Delete([FromRoute] string institution)
        {
            if(institution == null || institution == "")
                return BadRequest();

            var res = _resume.educations.FirstOrDefault(p => p.institution == institution);

            if(res == null){
                return NotFound();
            }

            _resume.educations.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Education> Update([FromBody] Education req)
        {
            if(req.institution == null || req.institution == "")
            {
                return BadRequest();
            }

            var Education = _resume.educations.FirstOrDefault( p => p.institution == req.institution);

            if(Education != null){
                _resume.educations.Remove(Education);
            }
            else{
                return NotFound();
            }

            _resume.educations.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }

        [HttpPatch("{institution}")]
        public ActionResult<Education> PartialUpdate([FromRoute] string institution, 
        [FromBody] JsonPatchDocument<Education> patchDocument)
        {
            if(institution == null  || institution == "" || patchDocument == null)
                return BadRequest();

            var obj = _resume.educations.FirstOrDefault(o => o.institution == institution);
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
