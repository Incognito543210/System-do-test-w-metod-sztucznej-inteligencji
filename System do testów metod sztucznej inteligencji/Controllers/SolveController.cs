﻿using Microsoft.AspNetCore.Mvc;
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


        public SolveController(ISolveService service)
        {
            _service = service;
        }

        [HttpPost("SolveOne{algorithmName}/{functionName}")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult Solve(string algorithmName, string functionName, [FromBody] SolveInput solveInput)
        {
            if (solveInput.Min.Length != solveInput.Max.Length)
            {
             
                return BadRequest("Min and Max muszą mieć tą samą długość.");
            }

            double[,] domain = new double[2, solveInput.Min.Length];

            for (int i = 0; i < solveInput.Min.Length; i++)
            {
                domain[0, i] = solveInput.Min[i];
                domain[1, i] = solveInput.Max[i];
            }
            double []result = _service.Solve(algorithmName, functionName, domain,solveInput.Parameters);

            return Ok(result);
        }


        [HttpPost("List")]
        [ProducesResponseType(200, Type = typeof(SolveOutput))]
        [ProducesResponseType(400)]
        public IActionResult SolveList( [FromBody]ICollection<SolveInput> solveInputList)
        {

            var result = _service.LIstOfSolve(solveInputList);
            return Ok(result);


        }
    }
}
