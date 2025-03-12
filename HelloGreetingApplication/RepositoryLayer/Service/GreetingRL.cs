using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Context;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        public readonly GreetingContext _DbContext;
        public GreetingRL(GreetingContext DbContext) 
        {
            _DbContext = DbContext; 
        }

        public string GreetingMessage(UserModel userModel)
        {
            var name = $"{userModel.FirstName} {userModel.LastName}".Trim();
            return string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";
        }

        public GreetingEntity AddGreeting(GreetingEntity greeting)
        {
            _DbContext.Greetings.Add(greeting);
            _DbContext.SaveChanges();
            return greeting;
        }

        public GreetingEntity GetGreetingById(Guid id)
        {
            var greet = _DbContext.Greetings.Find(id);
            if (greet == null)
            {
                return new GreetingEntity { Message = null };
            }
            return greet;
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            return _DbContext.Greetings.ToList();
        }

        public GreetingEntity UpdateGreeting(GreetingEntity greeting)
        {
            var existingGreeting = _DbContext.Greetings.Find(greeting.GreetingId);
            if (existingGreeting != null)
            {
                existingGreeting.Message = greeting.Message;
                _DbContext.SaveChanges();

                return existingGreeting;
            }

            return new GreetingEntity { Message = null };
           
        }

        public bool DeleteGreeting(Guid id)
        {
            var greeting = _DbContext.Greetings.Find(id);

            if (greeting != null)
            {
                _DbContext.Greetings.Remove(greeting);
                _DbContext.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
