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
    [Route("resume/basics/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private Resume _resume;

        public ProfilesController(ILogger<ProfilesController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Profile>> Get()
        {
            var res = _resume.basics.profiles;
            return Ok(res);
        }

        [HttpGet("{network}")]
        public ActionResult<List<Profile>> Get([FromRoute] string network)
        {
            if(network == null || network == "")
                return BadRequest();

            var res = _resume.basics.profiles.FirstOrDefault(p => p.network == network);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Profile> Create([FromBody] Profile req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();
            
            return Ok(req);
        }

        [HttpDelete("{network}")]
        public ActionResult<Profile> Delete([FromRoute] string network)
        {
            if(network == null || network == "")
                return BadRequest();

            var res = _resume.basics.profiles.FirstOrDefault(p => p.network == network);

            if(res == null){
                return NotFound();
            }

            _resume.basics.profiles.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Profile> Update([FromBody] Profile req)
        {
            if(req.network == null || req.network == "")
            {
                return BadRequest();
            }

            var Profile = _resume.basics.profiles.FirstOrDefault( p => p.network == req.network);

            if(Profile != null){
                _resume.basics.profiles.Remove(Profile);
            }
            else{
                return NotFound();
            }

            _resume.basics.profiles.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }
    }
}
