using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Laeridae_API.Models
{
    public class SchoolDBContext : DbContext
    {
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Students> Students { get; set; }
    }
}