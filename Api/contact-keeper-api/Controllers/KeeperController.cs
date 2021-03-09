using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace contact_keeper_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class KeeperController : ControllerBase
    {
        #region Users

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users")]
        public IActionResult RegisterUser([FromBody] User model)
        {
            try
            {
                var resp = new TokenResponse() { token = "1234567890" };
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("auth")]
        public IActionResult Login([FromBody] LoginDto model)
        {
            try
            {
                var resp = new TokenResponse() { token = "1234567890" };
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get a existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("auth")]
        public IActionResult FindUser([FromBody] LoginDto model)
        {
            try
            {
                var resp = new TokenResponse() { token = "3456" };
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        #endregion

    }
}
