using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ExamSetting
    {
        [Key]
        public int Id { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey("ExamId")]
        [Required]
        public string ExamId { get; set; }
        public DateTime AvailableDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool DisplayPoint { get; set; }
        public int NumOfSubmissions { get; set; }
        public string Password { get; set; }
        public DateTime FbDisplayAfter { get; set; }
        public DateTime FbDisplayBefore { get; set; }
    }
}
