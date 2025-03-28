﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public string LoginUser(LoginModel login);
        public RegistrationResponse RegisterUser(RegisterModel register);
        public bool ForgotPassword(string email);
        public bool ResetPassword(ResetPasswordModel resetPassword);
    }
}
