using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using Newtonsoft.Json;
using RepositoryLayer.Interface;
using Middleware.JWT;
using Middleware.Email;
using System.Web;
using System.Security.Claims;
using Middleware.PasswordHelper;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        private readonly ILogger<UserBL> _logger;
        private readonly JwtHelper _jwtToken;
        private readonly EmailService _emailService;
        public UserBL(IUserRL userRL, ILogger<UserBL> logger, JwtHelper jwtToken, EmailService emailService)
        {
            _userRL = userRL;
            _logger = logger;
            _jwtToken = jwtToken;
            _emailService = emailService;
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

        public bool ForgotPassword(string email)
        {
            var user = _userRL.GetUser(email);
            if (user == null) return false;

            string resetToken = _jwtToken.GenerateResetToken(user.Email);
            string encodedToken = HttpUtility.UrlEncode(resetToken);

            string emailBody = $"Your reset password token: {encodedToken}";

            _emailService.SendEmailAsync(user.Email, "Reset Your Password", emailBody);

            _logger.LogInformation("Reset password token sent via email to user: {Email}", email);
            return true;
        }

        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            var tokenData = _jwtToken.ValidateResetToken(resetPassword.Token);
            if (tokenData == null || !tokenData.ContainsKey(ClaimTypes.Email))
            {
                return false; 
            }

            string email = tokenData[ClaimTypes.Email].ToString();
            var user = _userRL.GetUser(email);
            if (user == null) return false;

            var salt = HashingPassword.GenerateSalt();
            user.Password = HashingPassword.HashPassword(resetPassword.NewPassword, salt);

            _userRL.UpdateUser(user);
            return true;
        }

    }
}
