using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class AdvancedFeedbacksInExam
    {
        public AdvancedFeedback AdvancedFeedback { get; set; }
        [Required]
        public int AdvancedFeedbackId { get; set; }
        public Exam Exam { get; set; }
        [Required]
        public string ExamId { get; set; }

    }
}
