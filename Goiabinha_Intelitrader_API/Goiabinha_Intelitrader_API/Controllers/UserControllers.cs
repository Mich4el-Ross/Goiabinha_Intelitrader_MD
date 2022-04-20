using Microsoft.AspNetCore.Mvc;
using Goiabinha_Intelitrader_API.Models;
using System.Net;

namespace Goiabinha_Intelitrader_API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserControllers : ControllerBase
    {
        private readonly AppDataContext _Context;
        private readonly ILogger<UserControllers> _Logger;


        public UserControllers(AppDataContext context, ILogger<UserControllers> logger)
        {
            _Context = context;
            _Logger = logger;
        }


        /// <summary>
        /// This function shows all the existing users datas on the database
        /// </summary>
        /// <response code="200">Users have been successfully returned</response>
        /// <response code="204">There is no registered users</response>
        /// <response code="400">Users datas haven't been found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Returns all the users datas</returns>
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _Context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, " - Error to GET users");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// This function shows a specific user through their id
        /// </summary>
        /// <param name="id">User ID</param>
        /// <response code="200">User data has been successfully returned</response>
        /// <response code="204">There is no registered user for the requested ID</response>
        /// <response code="400">User data hasn't been found<</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Returns the user data according to the id passed</returns>
        [HttpGet]
        [Route("users/{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            try
            {
                var user = await _Context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    return Ok(user);
                }

                _Logger.LogWarning($"The user_id({id}) - does not exist!");
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, " - Error to GET users");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }


        /// <summary>
        /// This function allows to create a new user on the database
        /// </summary>
        /// <param name="model">User data model template</param>
        /// <response code="200">User has been successfully registered</response>
        /// <response code="400">One of the required fields hasn't been completed</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Returns the body of the registered user</returns>
        [HttpPost]
        [Route("users")]
        public async Task<ActionResult<User>> PostUser(
            [FromBody] User model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _Context.Users.Add(model);
                    await _Context.SaveChangesAsync();
                    return Ok(model);
                }
                else
                {
                    _Logger.LogWarning("JSON model is invalid: " + model);
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, " - ERROR to POST a new user");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// This function allows to update a user data from the database
        /// </summary>
        /// <param name="model">User data model template</param>
        /// <param name="id">User ID</param>
        /// <response code="200">User data was updated successfull</response>
        /// <response code="204">There is no registered user for the requested ID</response>
        /// <response code="400">The template for modifying user data is invalid</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Return the user with their updated data.</returns>
        [HttpPut]
        [Route("users/{id}")]
        public async Task<ActionResult<User>> UpdateUser(
            [FromBody] User model, Guid id)
        {
            try
            {
                var user = await _Context.Users
                     .AsNoTracking()
                     .FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    user.FirstName    = model.FirstName;
                    user.SurName      = model.SurName;
                    user.Age          = model.Age;
                    user.CreationDate = DateTime.Now;
                    _Context.Users.Update(user);
                    await _Context.SaveChangesAsync();
                    return Ok(user);
                }

                _Logger.LogWarning($"The user_id({id}) - does not exist!");
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, " - ERROR to update users.");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// This function allows to remove a user from the database
        /// </summary>
        /// <param name="id">User ID</param>
        /// <response code="200">User data was found successfully</response>
        /// <response code="204">There is no registered user for the requested ID</response>
        /// <response code="400">User data was not found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Returns the deleted user data</returns>
        [HttpDelete]
        [Route("users/{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            try
            {
                var user = await _Context.Users
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    _Context.Users.Remove(user);
                    await _Context.SaveChangesAsync();
                    return Ok(user);
                }

                _Logger.LogWarning($"The user_id({id}) - does not exist!");
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, " - ERROR to DELETE user");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

    }
}