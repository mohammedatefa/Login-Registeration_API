using Login_Register_Api.Context;
using Login_Register_Api.Model;
using Login_Register_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login_Register_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class user : ControllerBase
    {
        private LoginContext Context;

        private Token Token;

        public user(LoginContext _context,Token _token)
        {
            Context = _context;
            Token = _token;
        }
        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn(User model)
        {
            User found = Context.Users.FirstOrDefault(u => u.Email== model.Email);
            if (ModelState.IsValid)
            {
                if (found == null)
                {
                    try
                    {
                        Context.Add(model);
                        Context.SaveChanges();
                        return Ok(new { message = "Success" });
                    }
                    catch
                    {
                        return Ok(new { message = "An error occurred while adding the user" });
                    }
                }
                else
                {
                    return Ok(new { message = " The email is already in use" });
                }
            }
            return BadRequest(new { message = "Validation errors" });
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login model)
        {

            var user = Context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                // User not found or credentials are incorrect
                return Ok(new { message = "there is not account with this email" });
            }

            // Generate a token
            string token = Token.GenerateToken(user);
            //create sucess message
            string message = "Success";

            // Return the token and user data
            var response = new
            {
                Token = token,
                User = user,
                Message = message
            };

            return Ok(response);
        }
    }
}
