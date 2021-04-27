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
        public DbSet<Subject> Subjects { set; get; }
        public DbSet<Test> Tests { set; get; }
        public DbSet<TestSummary> TestsResults { set; get; }
        public DbSet<TestItem> TestItems { set; get; }
        public DbSet<TestItemOption> TestItemOptions { set; get; }
    }
}
