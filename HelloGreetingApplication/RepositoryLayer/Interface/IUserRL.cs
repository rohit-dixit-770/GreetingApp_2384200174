using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public string LoginUser(LoginModel login);
        public RegistrationResponse RegisterUser(RegisterModel register);
        public UserEntity GetUser(string email);
        public void UpdateUser(UserEntity user);
    }
}
