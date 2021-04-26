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
    public class BasicsController : ControllerBase
    {
        private readonly ILogger<BasicsController> _logger;
        private Resume _resume;

        public BasicsController(ILogger<BasicsController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<Basic> Get()
        {
            Basic res = _resume.basics;
            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Basic> Create([FromBody] Basic req)
        {  
            if(_resume.basics == null)
                _resume.basics = req;
            else
                Conflict();
            
            return Ok(req);
        }

        [HttpDelete]
        public ActionResult<Basic> Delete()
        {
            Basic res = _resume.basics;
            _resume.basics = null;
            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Basic> Update([FromBody] Basic req)
        {
            _resume.basics = req;
            return Ok(req);
        }

        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
    }
}
