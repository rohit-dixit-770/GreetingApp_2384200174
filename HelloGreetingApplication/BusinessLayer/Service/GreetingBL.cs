using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using ModelLayer.Model;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;
        public GreetingBL(IGreetingRL greetingRL) 
        {
            _greetingRL = greetingRL;
        }

        public string GreetingMessage()
        {
            var result = _greetingRL.GreetingMessage();
            return result;
        }
    }
}
