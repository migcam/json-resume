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
    public class ReferencesController : ControllerBase
    {
        private readonly ILogger<ReferencesController> _logger;
        private Resume _resume;

        public ReferencesController(ILogger<ReferencesController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Reference>> Get()
        {
            var res = _resume.references;
            return Ok(res);
        }

        [HttpGet("{name}")]
        public ActionResult<List<Reference>> Get([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.references.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Reference> Create([FromBody] Reference req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.references.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{name}")]
        public ActionResult<Reference> Delete([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.references.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            _resume.references.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Reference> Update([FromBody] Reference req)
        {
            if(req.name == null || req.name == "")
            {
                return BadRequest();
            }

            var Reference = _resume.references.FirstOrDefault( p => p.name == req.name);

            if(Reference != null){
                _resume.references.Remove(Reference);
            }
            else{
                return NotFound();
            }

            _resume.references.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }

        [HttpPatch("{name}")]
        public ActionResult<Reference> PartialUpdate([FromRoute] string name, 
        [FromBody] JsonPatchDocument<Reference> patchDocument)
        {
            if(name == null  || name == "" || patchDocument == null)
                return BadRequest();

            var obj = _resume.references.FirstOrDefault(o => o.name == name);
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
