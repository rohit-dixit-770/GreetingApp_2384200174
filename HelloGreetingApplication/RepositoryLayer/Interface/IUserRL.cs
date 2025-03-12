using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public string LoginUser(LoginModel login);
        public RegistrationResponse RegisterUser(RegisterModel register);
        public string ForgotPassword(ForgotPasswordModel forgotPassword);
        public bool ResetPassword(ResetPasswordModel resetPassword);
    }
}
