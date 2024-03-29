﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using score.Domain.Autentication.Login;
using Serilog;

namespace score.Controllers
{
    /// <summary>
    /// Controller for User Login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {

        private readonly IMediator mediator;

        public AutenticationController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
        }


        // POST: api/Autentication
        /// <summary>
        /// Enpoint for authorization token. 
        /// </summary>
        /// <param name="command">Class that contains fields for user validation</param>
        /// <returns>Objetct with token</returns>
        /// <response code="200">Object with a valid token</response>
        /// <response code="400">Input string error</response>
        /// <response code="500">Error generated by application</response>
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginCommand command)
        {
            if (command is null)
            {
                return BadRequest("Input string error sintax");
            }

            Log.Logger.Information($"Login User: {command.email}");
            var response = mediator.Send(command);
            if (response.Result.IsSuccess)
            {
                return Ok(response.Result);
            }
            else
            {
                return StatusCode(500, response.Result);
            }
        }

    }
}
