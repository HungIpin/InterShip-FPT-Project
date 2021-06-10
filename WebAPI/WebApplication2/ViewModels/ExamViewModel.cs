using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class ExamViewModel
    {
        public List<Exam> Exams { get; set; }
        public List<Certification> Certifications { get; set; }

        public List<FeedbackType> FeedbackTypes { get; set; }
        public List<FeedbackLevel> FeedbackLevels { get; set; }
        public List<Account> Accounts { get; set; }

        public List<ScoreRecording> ScoreRecordings { get; set; }
    }
}
