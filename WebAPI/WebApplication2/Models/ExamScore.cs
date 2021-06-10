
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ExamScore
    {
        [Key]
        public int Id { get; set; }
        public Account Account { get; set; }
        [Required]
        public int AccountId { get; set; }

        public Exam Exam { get; set; }
        [Required]
        public string ExamId { get; set; }
        public double Points { get; set; }
        public DateTime OpenedTime { get; set; }
        public DateTime ClosedTime { get; set; }

    }
}
