#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.Models;
using System.Data;

namespace Demo.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext (DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<Demo.Models.Cliente> Cliente { get; set; }
    }
}
