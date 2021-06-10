using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ExamHistory
    {
        public ExamHistory()
        {
            ExamHistoryDetails = new HashSet<ExamHistoryDetail>();
        }
        [Key]
        public int Id { get; set; }
        public Account Account { get; set; }
        [Required]
        public int AccountId { get; set; }
        public QuestionsInPart QuestionsInPart { get; set; }
        [Required]
        public int QuestionsInPartId { get; set; }
        public ICollection<ExamHistoryDetail> ExamHistoryDetails { get; set; }
        public float Points { get; set; }
    }
}
