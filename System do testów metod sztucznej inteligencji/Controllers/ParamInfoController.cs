using Microsoft.AspNetCore.Mvc;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ParamInfoController : ControllerBase
    {
        private readonly IParamInfoService _paramInfoService;

        public ParamInfoController(IParamInfoService paramInfoService)
        {
            _paramInfoService = paramInfoService;
        }

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ParamInfo))]
        [ProducesResponseType(400)]
        public IActionResult GetParamInfo(string name)
        {
            if(!_paramInfoService.ParamInfoExist(name))
            {
                return NotFound();
            }

            var ParamInfo = _paramInfoService.GetParamInfo(name);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ParamInfo);
        }
      

    }
}
