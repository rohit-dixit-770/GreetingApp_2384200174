using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        public string GreetingMessage(UserModel userModel);
        public GreetingEntity AddGreeting(GreetingEntity greeting);
        public GreetingEntity GetGreetingById(Guid id);
    }
}
