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
using System.Security.Claims;

namespace contact_keeper_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class KeeperController : ControllerBase
    {
        #region Users & Authentication

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
        /// View the current list of users stored in the database (using MS SQL SERVER localhost)
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
                var tsql = new Query();
                var tokenHandler = new TokenHandler();

                #region validation

                if (string.IsNullOrWhiteSpace(model.email))
                    throw new NullReferenceException("Email is required");

                if (string.IsNullOrWhiteSpace(model.password))
                    throw new NullReferenceException("Password is required");

                if (!string.IsNullOrWhiteSpace(model.password) && model.password.Length < 6)
                    throw new NullReferenceException("Please enter a password with 6 or more characters");

                if (!Regex.IsMatch(model.email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    throw new NullReferenceException("A valid email is required");
                #endregion

                var user = tsql.GetUsers()
                    .FirstOrDefault(a => a.email.ToLower().Trim() == model.email.ToLower().Trim()
                    && a.password == model.password);

                if (user is null)
                    throw new NullReferenceException("Incorect email/password");

                var resp = new TokenResponse() { token = tokenHandler.generateJwtToken(user) };
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
        /// Get an existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [JWT.Authorize]
        [HttpGet]
        [Route("auth")]
        public IActionResult FindUser()
        {
            try
            {
                var tsql = new Query();
                var jwt = new JwtMiddleware();
                var token = Request.Headers["x-auth-token"].FirstOrDefault()?.Split(" ").Last();

                var Id = jwt.GetClaimValue(token);

                var resp = tsql.GetUsers().FirstOrDefault(a => a.Id == Id);

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        #endregion

        #region Contacts


        /// <summary>
        /// Returns list of contacts for a specific users
        /// </summary>
        /// <returns></returns>
        [JWT.Authorize]
        [HttpGet]
        [Route("contacts")]
        public IActionResult GetContacts()
        {
            try
            {
                var tsql = new Query();
                var jwt = new JwtMiddleware();
                var token = Request.Headers["x-auth-token"].FirstOrDefault()?.Split(" ").Last();

                var user = jwt.GetClaimValue(token);

                var result = tsql.GetContacts(user);
                return Ok(result);
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

        [JWT.Authorize]
        [HttpPost]
        [Route("contacts")]
        public IActionResult AddNewContact([FromBody] Contact model)
        {
            try
            {
                var tsql = new Query();
                var tokenHandler = new TokenHandler();

                #region validation
                if (string.IsNullOrWhiteSpace(model.Name))
                    throw new NullReferenceException("Name is required");

                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                        throw new NullReferenceException("A valid email is required");
                }

                var jwt = new JwtMiddleware();
                var token = Request.Headers["x-auth-token"].FirstOrDefault()?.Split(" ").Last();

                var user = jwt.GetClaimValue(token);
                model.Owner = user;

                #endregion validation

                var result = tsql.InsertNewContact(model);
                return Ok(result);
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

        [JWT.Authorize]
        [HttpPost]
        [Route("contacts/update")]
        public IActionResult UpdateContact([FromBody] Contact model)
        {
            try
            {
                var tsql = new Query();
                var tokenHandler = new TokenHandler();

                #region validation
                try { Guid.Parse(model.Id.ToString()); }
                catch
                {
                    throw new NullReferenceException("Invalid contact id");
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                    throw new NullReferenceException("Name is required");

                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                        throw new NullReferenceException("A valid email is required");
                }

                var jwt = new JwtMiddleware();
                var token = Request.Headers["x-auth-token"].FirstOrDefault()?.Split(" ").Last();

                var user = jwt.GetClaimValue(token);
                model.Owner = user;

                #endregion validation

                var result = tsql.UpdateContact(model);
                return Ok(result);
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

        [JWT.Authorize]
        [HttpDelete("contacts/{id}")]
        [Route("contacts")]
        public IActionResult DeleteContact([FromRoute] string id)
        {
            try
            {
                var tsql = new Query();
                var tokenHandler = new TokenHandler();

                #region validation
                try { Guid.Parse(id); }
                catch
                {
                    throw new NullReferenceException("Invalid contact id");
                }
                var guid = Guid.Parse(id);
                #endregion validation

                var result = tsql.DeleteContact(guid);
                return Ok(new { Message = result ? "Successfully removed contact from storage" : "Failed to update contact details", Succses = result, });
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
        #endregion
    }
}
