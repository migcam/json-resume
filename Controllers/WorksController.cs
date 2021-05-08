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
    public class WorksController : ControllerBase
    {
        private readonly ILogger<WorksController> _logger;
        private Resume _resume;

        public WorksController(ILogger<WorksController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Work>> Get()
        {
            var res = _resume.works;
            return Ok(res);
        }

        [HttpGet("{company}")]
        public ActionResult<List<Work>> Get([FromRoute] string company)
        {
            if(company == null || company == "")
                return BadRequest();

            var res = _resume.works.FirstOrDefault(p => p.company == company);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Work> Create([FromBody] Work req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.works.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{company}")]
        public ActionResult<Work> Delete([FromRoute] string company)
        {
            if(company == null || company == "")
                return BadRequest();

            var res = _resume.works.FirstOrDefault(p => p.company == company);

            if(res == null){
                return NotFound();
            }

            _resume.works.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Work> Update([FromBody] Work req)
        {
            if(req.company == null || req.company == "")
            {
                return BadRequest();
            }

            var Work = _resume.works.FirstOrDefault( p => p.company == req.company);

            if(Work != null){
                _resume.works.Remove(Work);
            }
            else{
                return NotFound();
            }

            _resume.works.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
    
        [HttpPatch("{company}")]
        public ActionResult<Work> PartialUpdate([FromRoute] string company, 
        [FromBody] JsonPatchDocument<Work> patchDocument)
        {
            if(company == null  || company == "" || patchDocument == null)
                return BadRequest();

            var obj = _resume.works.FirstOrDefault(o => o.company == company);
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
