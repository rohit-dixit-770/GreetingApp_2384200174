﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;
        public GreetingBL(IGreetingRL greetingRL) 
        {
            _greetingRL = greetingRL;
        }

        public string GreetingMessage(MessageModel messageModel)
        {
            var result = _greetingRL.GreetingMessage(messageModel);
            return result;
        } 

        public GreetingEntity AddGreeting(GreetingEntity greeting)
        {
            var result = _greetingRL.AddGreeting(greeting);
            return result;
            
        }

        public GreetingEntity GetGreetingById(Guid id)
        {
            var greet = _greetingRL.GetGreetingById(id);
            return greet;
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            return _greetingRL.GetAllGreetings();
        }

        public GreetingEntity UpdateGreeting(GreetingEntity greeting)
        {
            var greet = _greetingRL.UpdateGreeting(greeting);
            return greet;
        }

        public bool DeleteGreeting(Guid id)
        {
            var deletedGreet = _greetingRL.DeleteGreeting(id);
            return deletedGreet;
        }
    }
}
