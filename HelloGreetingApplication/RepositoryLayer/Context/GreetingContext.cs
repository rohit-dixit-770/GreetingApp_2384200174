using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class GreetingContext : DbContext
    {
        public GreetingContext(DbContextOptions<GreetingContext> options) : base(options) 
        {
        }
        public virtual DbSet<GreetingEntity> Greetings { get; set; }

    }
}
