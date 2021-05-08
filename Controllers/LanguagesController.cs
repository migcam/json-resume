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
    public class LanguagesController : ControllerBase
    {
        private readonly ILogger<LanguagesController> _logger;
        private Resume _resume;

        public LanguagesController(ILogger<LanguagesController> logger,
        Resume resume)
        {
            _logger = logger;
            _resume = resume;
        }

        [HttpGet]
        public ActionResult<List<Language>> Get()
        {
            var res = _resume.languages;
            return Ok(res);
        }

        [HttpGet("{language}")]
        public ActionResult<List<Language>> Get([FromRoute] string language)
        {
            if(language == null || language == "")
                return BadRequest();

            var res = _resume.languages.FirstOrDefault(p => p.language == language);

            if(res == null){
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost] 
        public ActionResult<Language> Create([FromBody] Language req)
        {  
            if(req == null)
                return BadRequest();
            else
                Conflict();

            _resume.languages.Add(req);
            
            return Ok(req);
        }

        [HttpDelete("{language}")]
        public ActionResult<Language> Delete([FromRoute] string language)
        {
            if(language == null || language == "")
                return BadRequest();

            var res = _resume.languages.FirstOrDefault(p => p.language == language);

            if(res == null){
                return NotFound();
            }

            _resume.languages.Remove(res);

            return Ok(res);
        }

        [HttpPut]
        public ActionResult<Language> Update([FromBody] Language req)
        {
            if(req.language == null || req.language == "")
            {
                return BadRequest();
            }

            var Language = _resume.languages.FirstOrDefault( p => p.language == req.language);

            if(Language != null){
                _resume.languages.Remove(Language);
            }
            else{
                return NotFound();
            }

            _resume.languages.Add(req);

            return Ok(req);
        }

        
        [HttpHead]
        public ActionResult Head()
        {
            return NoContent();
        }

        [HttpPatch("{language}")]
        public ActionResult<Language> PartialUpdate([FromRoute] string language, 
        [FromBody] JsonPatchDocument<Language> patchDocument)
        {
            if(language == null  || language == "" || patchDocument == null)
                return BadRequest();

            var obj = _resume.languages.FirstOrDefault(o => o.language == language);
            patchDocument.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
