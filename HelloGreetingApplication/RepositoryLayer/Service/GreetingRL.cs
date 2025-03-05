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
    }
}
