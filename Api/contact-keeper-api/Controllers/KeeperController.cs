using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Keeper.Data.Data;
using Keeper.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using contact_keeper_api.JWT;

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
                var tsql = new Query();
                var tokenHandler = new TokenHandler();

                #region validation
                if (string.IsNullOrWhiteSpace(model.name))
                    throw new NullReferenceException("Name is required");

                if (string.IsNullOrWhiteSpace(model.email))
                    throw new NullReferenceException("Email is required");

                if (string.IsNullOrWhiteSpace(model.password))
                    throw new NullReferenceException("Password is required");

                if (!string.IsNullOrWhiteSpace(model.password) && model.password.Length < 6)
                    throw new NullReferenceException("Please enter a password with 6 or more characters");

                if (!Regex.IsMatch(model.email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    throw new NullReferenceException("A valid email is required");


                var user = tsql.GetUsers()
                    .FirstOrDefault(a => a.email.ToLower().Trim() == model.email.ToLower().Trim());

                if (!(user is null))
                    throw new NullReferenceException("User already exists");
                #endregion validation

                var result = tsql.InsertNewUser(model);
                var resp = new TokenResponse() { token = tokenHandler.generateJwtToken(result) };
                return Ok(resp);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new Error
                {
                    Message = ex.Message,
                    Status = false
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Error
                    {
                        Message = ex.Message,
                        Status = false
                    });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [JWT.Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult AllUsers()
        {
            try
            {
                var tsql = new Query();
                var result = tsql.GetUsers();
                return Ok(result);
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
        /// 
        [JWT.Authorize]
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
