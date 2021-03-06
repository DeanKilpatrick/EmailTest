using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmailTest.Models;

namespace EmailTest.Data
{
    public class EmailTestContext : DbContext
    {
        public EmailTestContext (DbContextOptions<EmailTestContext> options)
            : base(options)
        {
        }

        
    }
}
