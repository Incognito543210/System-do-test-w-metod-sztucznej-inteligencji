using Microsoft.AspNetCore.Mvc;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DllController  : ControllerBase
    {
        private readonly IDllService _dllService;
        public DllController(IDllService dllService) 
        {
            _dllService = dllService;
        }

        [HttpPost("{folderPath}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult AddDllFile([FromBody] DllFiles dllFiles, string folderPath)
        {

            string[] dllPathFiles = Directory.GetFiles(folderPath, "*.dll");

            foreach (string dllFile in dllPathFiles)
            {
                string fileName = Path.GetFileName(dllFile);
                string destinationPath = Path.Combine(folderPath, fileName);
            }

            return Ok("Received successfully");
        }
    }
}
