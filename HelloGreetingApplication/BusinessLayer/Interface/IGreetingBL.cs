﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        public string GreetingMessage(UserModel userModel);
        public GreetingEntity AddGreeting(GreetingEntity greeting);
    }
}
