using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTests.Models
{
    public class StudentSubjectSummary
    {
        public Test Test { get; set; }

        public int attempts { get; set; }

        public int maxMark { get; set; }

        public DateTime lastAttempt { get; set; }
    }
}
