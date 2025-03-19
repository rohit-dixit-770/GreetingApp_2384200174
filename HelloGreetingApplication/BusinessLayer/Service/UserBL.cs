using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using Middleware.JWT;  

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        private readonly ILogger<UserBL> _logger;
        private readonly JwtHelper _jwtHelper;

        public UserBL(IUserRL userRL, ILogger<UserBL> logger, JwtHelper jwtHelper)
        {
            _userRL = userRL;
            _logger = logger;
            _jwtHelper = jwtHelper;
        }

        public string LoginUser(LoginModel login)
        {

                string result = _userRL.LoginUser(login);
                _logger.LogInformation("Login successful for user: {Email}", login.Email);
                return result;

        }

        public RegistrationResponse RegisterUser(RegisterModel register)
        {
                var result = _userRL.RegisterUser(register);
            _logger.LogInformation("Registration successful for user: {Email}", register.Email);

            return result;
        }

        public string ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            var result = _userRL.ForgotPassword(forgotPassword);

            return result;
        }

        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            return _userRL.ResetPassword(resetPassword);
        }
    }
}
