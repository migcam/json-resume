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
    public class SkillsController : ControllerBase
    {
        private readonly ILogger<SkillsController> _logger;
        private Resume _resume;

        public SkillsController(ILogger<SkillsController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Skill>> Get()
        {
            var res = _resume.skills;
            return Ok(res);
        }

        [HttpGet("{name}")]
        public ActionResult<List<Skill>> Get([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.skills.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Skill> Create([FromBody] Skill req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();
            
            _resume.skills.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{name}")]
        public ActionResult<Skill> Delete([FromRoute] string name)
        {
            if(name == null || name == "")
                return BadRequest();

            var res = _resume.skills.FirstOrDefault(p => p.name == name);

            if(res == null){
                return NotFound();
            }

            _resume.skills.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Skill> Update([FromBody] Skill req)
        {
            if(req.name == null || req.name == "")
            {
                return BadRequest();
            }

            var Skill = _resume.skills.FirstOrDefault( p => p.name == req.name);

            if(Skill != null){
                _resume.skills.Remove(Skill);
            }
            else{
                return NotFound();
            }

            _resume.skills.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }

        [HttpPatch("{name}")]
        public ActionResult<Skill> PartialUpdate([FromRoute] string name, 
        [FromBody] JsonPatchDocument<Skill> patchDocument)
        {
            if(name == null  || name == "" || patchDocument == null)
                return BadRequest();

            var obj = _resume.skills.FirstOrDefault(o => o.name == name);
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
