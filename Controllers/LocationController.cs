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
    [Route("resume/basics/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private Resume _resume;

        public LocationController(ILogger<LocationController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<Location> Get()
        {
            Location res = _resume.basics.location;
            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Location> Create([FromBody] Location req)
        {  
            if(_resume.basics.location == null)
                _resume.basics.location = req;
            else
                Conflict();
            
            return Ok(req);
        }

        [HttpDelete]
        public ActionResult<Location> Delete()
        {
            Location res = _resume.basics.location;
            _resume.basics.location = null;
            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Location> Update([FromBody] Location req)
        {
            _resume.basics.location = req;
            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }

        [HttpPatch]
        public ActionResult<Location> PartialUpdate([FromBody] JsonPatchDocument<Location> patchDocument)
        {
            if(patchDocument == null)
                return BadRequest();

            var obj = _resume.basics.location;
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
