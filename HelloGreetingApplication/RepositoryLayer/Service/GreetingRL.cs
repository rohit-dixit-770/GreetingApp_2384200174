using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {

        public string GreetingMessage(string FirstName, string LastName)
        {
            var name = $"{FirstName} {LastName}".Trim();
            return string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";
        }
    }
}
