using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ExamHistoryDetail
    {
        [Key]
        public int Id { get; set; }
        public ExamHistory ExamHistory { get; set; }
        [Required]
        public int ExamHistoryId { get; set; }
        public string Choice { get; set; }
    }
}
