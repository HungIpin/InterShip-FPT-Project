using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class QuestionsInPart
    {
        [Key]
        public int Id { get; set; }
        public Question Question { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public ExamPart ExamPart { get; set; }
        [Required]
        public int ExamPartId { get; set; }
        public int SequenceNo { get; set; }
        public ICollection<ExamHistory> ExamHistories { get; set; }
    }
}
