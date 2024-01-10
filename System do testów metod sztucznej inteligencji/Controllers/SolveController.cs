using Microsoft.AspNetCore.Mvc;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using System_do_testów_metod_sztucznej_inteligencji.Services;

namespace System_do_testów_metod_sztucznej_inteligencji.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class SolveController : ControllerBase
    {

        private readonly ISolveService _service;
        private readonly IStateReader _stateReader;


        public SolveController(ISolveService service, IStateReader stateReader)
        {
            _service = service;
            _stateReader = stateReader;
        }

        
        [HttpPost("List")]
        [ProducesResponseType(200, Type = typeof(SolveOutput))]
        [ProducesResponseType(400)]
        public IActionResult SolveList( [FromBody]ICollection<SolveInput> solveInputList)
        {
            if (solveInputList == null)
            {
                return BadRequest("Przesłana lista nie może być pusta");
            }

            var result = _service.LIstOfSolve(solveInputList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(result == null)
            {
                return BadRequest("Coś poszło nie tak");
            }

            return Ok(result);


        }

        [HttpGet("Resume")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public IActionResult CanResume()
        {
            var FilesExists = _stateReader.FilesExist();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(FilesExists);
        }

        [HttpPost("Resume")]
        [ProducesResponseType(200, Type = typeof(SolveOutput))]
        [ProducesResponseType(400)]
        public IActionResult DoResume([FromBody] bool doResume)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (doResume)
            {
                var result = _service.LIstOfSolve(_service.Resume());
                return Ok(result);
            }
            else
            {
                _stateReader.DelateCombinationFiles();
                _stateReader.DelateFiles();
                return Ok("Usunieto pliki");
            }
          

           
        }

    }
}
