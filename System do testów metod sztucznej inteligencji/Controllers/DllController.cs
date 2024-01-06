using Microsoft.AspNetCore.Mvc;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using System_do_testów_metod_sztucznej_inteligencji.Services;

namespace System_do_testów_metod_sztucznej_inteligencji.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DllController : ControllerBase
    {
        private readonly IDllService _dllService;
        public DllController(IDllService dllService)
        {
            _dllService = dllService;
        }

        [HttpPost("AddDLLFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult AddDllFile([FromBody] DllFile dllFilesCreate)
        {

            if(dllFilesCreate == null)
            {
                return BadRequest("Popraw wprowdzone dane, nie mogą być puste");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }

            if(_dllService.DllFileExist(dllFilesCreate.DllName))
            {
                return BadRequest("Plik istnieje");
            }

            if(!_dllService.CreateDllFile(dllFilesCreate))
            {
                return BadRequest("Coś poszło nie tak podczas zapisywania");
            }
   
            return Ok("Zapisano plik");
        }

  


        [HttpGet("algorithmList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DllFile>))]
        [ProducesResponseType(400)]
        public IActionResult GetListOfDLlAlgorithm()
        {
            if (!_dllService.AnyAlgorithmExist())
            {
               return NotFound("Brak algorytmów");
           }

            var listOfAlgorithm = _dllService.GetAlgorithmFiles();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(listOfAlgorithm);
        }

        [HttpGet("functionList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DllFile>))]
        [ProducesResponseType(400)]
        public IActionResult GetListOfDLlFunction()
        {
            if (!_dllService.AnyFunctionExist())
            {
                return NotFound("Brak funkcji testowych");
            }

            var listOfFunction = _dllService.GetFunctionFiles();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(listOfFunction);
        }



        [HttpDelete("{DLLName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDLLFile(string DLLName)
        {
            if(!_dllService.DllFileExist(DLLName))
            {
                return NotFound("Nie ma takiego pliku");
            }
            var DllFileToRemove = _dllService.GetDllFile(DLLName);

            if(!_dllService.DeleteDLLFile(DllFileToRemove))
            {
                return BadRequest("Coś poszło nie tak z usówaniem");
            }

            return Ok("Usunięto plik pomyślnie");


        }


    }
}