using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class ExamScoreViewModel
    {
        public List<ExamScore> ExamScores { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Exam> Exams { get; set; }
    }
}
