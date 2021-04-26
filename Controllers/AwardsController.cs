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
    public class AwardsController : ControllerBase
    {
        private readonly ILogger<AwardsController> _logger;
        private Resume _resume;

        public AwardsController(ILogger<AwardsController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Award>> Get()
        {
            var res = _resume.awards;
            return Ok(res);
        }

        [HttpGet("{title}")]
        public ActionResult<List<Award>> Get([FromRoute] string title)
        {
            if(title == null || title == "")
                return BadRequest();

            var res = _resume.awards.FirstOrDefault(p => p.title == title);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Award> Create([FromBody] Award req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.awards.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{title}")]
        public ActionResult<Award> Delete([FromRoute] string title)
        {
            if(title == null || title == "")
                return BadRequest();

            var res = _resume.awards.FirstOrDefault(p => p.title == title);

            if(res == null){
                return NotFound();
            }

            _resume.awards.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Award> Update([FromBody] Award req)
        {
            if(req.title == null || req.title == "")
            {
                return BadRequest();
            }

            var Award = _resume.awards.FirstOrDefault( p => p.title == req.title);

            if(Award != null){
                _resume.awards.Remove(Award);
            }
            else{
                return NotFound();
            }

            _resume.awards.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
    }
}
