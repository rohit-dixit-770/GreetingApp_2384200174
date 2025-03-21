﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserBL userBL)
        {
            _userBL = userBL;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginModel login)
        {

             _logger.LogInformation("Login attempt for user: {Username}", login.Email);
            var result = _userBL.LoginUser(login);
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Data = result
                });
            
        }

        [HttpPost("register")]
        public IActionResult RegistrationUser([FromBody] RegisterModel register)
        {

                var result = _userBL.RegisterUser(register);

                var response = new ResponseModel<RegistrationResponse>
                {
                    Success = true,
                    Message = "Registration successful",
                    Data = result
                };

                return Created("User created", response);

        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword( ForgotPasswordModel forgotPassword)
        {

                var result = _userBL.ForgotPassword(forgotPassword.Email);
            return Ok(new ResponseModel<bool>
            {
                Success = true,
                Message = $"Reset token is Sent to your Email: {forgotPassword.Email}",
                Data = result
            });

        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword( ResetPasswordModel resetPassword)
        {

                var result = _userBL.ResetPassword(resetPassword);
                return Ok(new ResponseModel<string>
                {
                    Success = result,
                    Message = result ? "Password reset successfully" : "Invalid token or expired",
                });

        }


    }
}
