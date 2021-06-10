using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AdvancedFeedbacksInExamViewModel
    {
        public List<AdvancedFeedbacksInExam> AdvancedFeedbacksInExams { get; set; }
        public List<AdvancedFeedback> AdvancedFeedbacks { get; set; }

        public List<Exam> Exams { get; set; }
    }
}
