using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OnlineTests.Models
{
    public class DBContext: DbContext
    {
        public DbSet<BaseUser> Users { set; get; }
    }
}
