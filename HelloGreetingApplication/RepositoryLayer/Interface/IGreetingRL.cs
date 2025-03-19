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
        public string GreetingMessage(MessageModel messageModel);
        public GreetingEntity AddGreeting(GreetingEntity greeting);
        public GreetingEntity GetGreetingById(Guid id);
        public List<GreetingEntity> GetAllGreetings();
        public GreetingEntity UpdateGreeting(GreetingEntity greeting);
        public bool DeleteGreeting(Guid id);

    }
}
