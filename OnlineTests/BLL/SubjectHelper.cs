using OnlineTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTests.BLL
{
    public class SubjectHelper
    {
        DBContext db;

        public SubjectHelper()
        {
            db = new DBContext();
        }

        public List<SubjectSummaryModel> GetSummaryForSubject (int subjectId)
        {
            var allTest = db.Tests.Where(t => t.SubjectId == subjectId);
            List<SubjectSummaryModel> result = new List<SubjectSummaryModel>();
            foreach (var student in db.Users)
            {
                int sum = 0;
                foreach (var test in allTest)
                {
                    var results = db.TestsResults.Where(tr => tr.OwnerUserId == student.Id && tr.TestId == test.Id);
                    var maxMark = results.DefaultIfEmpty().Max(tr => tr == null ? 0 : tr.Points);
                    sum += maxMark;
                    
                    
                }
                if(sum > 0)
                {
                    SubjectSummaryModel subjectSummaryModel = new SubjectSummaryModel
                    {
                        Student = student,
                        Mark = sum
                    };
                    result.Add(subjectSummaryModel);
                }
            }
            return result;
        }

        public List<StudentSubjectSummary> GetSummaryForStudentAndSubject(int studentId, int subjectId)
        {
            var allTest = db.Tests.Where(t => t.SubjectId == subjectId);
            List<StudentSubjectSummary> result = new List<StudentSubjectSummary>();
            foreach(var test in allTest)
            {
                StudentSubjectSummary summary = new StudentSubjectSummary();
                summary.Test = test;
                summary.attempts = db.TestsResults.Where(tr => tr.OwnerUserId == studentId && tr.TestId == test.Id).Count();
                if(summary.attempts > 0)
                {
                    summary.maxMark = db.TestsResults.Where(tr => tr.OwnerUserId == studentId && tr.TestId == test.Id).DefaultIfEmpty().Max(tr => tr == null ? 0 : tr.Points);
                    summary.lastAttempt = db.TestsResults.Where(tr => tr.OwnerUserId == studentId && tr.TestId == test.Id).DefaultIfEmpty().Max(tr => tr == null ? DateTime.MinValue : tr.CreatedDateTime);
                }
                else
                {
                    summary.maxMark = 0;
                    summary.lastAttempt = new DateTime();
                }

                result.Add(summary);
            }

            return result;
        }


        public List<LearningSummary> GetLearningSummary(int studentId)
        {
            List<LearningSummary> result = new List<LearningSummary>();
            foreach(var subject in db.Subjects)
            {
                var allTest = db.Tests.Where(t => t.SubjectId == subject.Id);
                int sum = 0;
                foreach (var test in allTest)
                {
                    var maxMark = db.TestsResults.Where(tr => tr.OwnerUserId == studentId && tr.TestId == test.Id).DefaultIfEmpty().Max(tr => tr == null ? 0 : tr.Points);
                    sum += maxMark;
                }
                if (studentId != subject.TeacherId)
                {
                    LearningSummary learningSummary = new LearningSummary
                    {
                        Subject = subject,
                        Mark = sum
                    };
                    result.Add(learningSummary);
                }
            }

            return result;
        }
    }
}
