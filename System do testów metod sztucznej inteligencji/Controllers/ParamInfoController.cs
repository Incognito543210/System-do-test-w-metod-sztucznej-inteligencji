﻿using Microsoft.AspNetCore.Mvc;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using System_do_testów_metod_sztucznej_inteligencji.Services;

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
        public IActionResult GetParamsInfo(string name)
        {
           

            var paramsInfo = _paramInfoService.GetParamsInfo(name);


            if(paramsInfo == null)
            {
                return BadRequest("Puste parametry");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(paramsInfo);
        }


    


    }
}
