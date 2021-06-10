using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class QuestionSetting
    {
        [Key]
        public int Id { get; set; }
        public float PointValue { get; set; }
        public bool DisplayPoint { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CorrectFb { get; set; }
        public string InCorrectFb { get; set; }
        public float DeductedPoints { get; set; }
        public Question Question { get; set; }
        [ForeignKey("QuestionId")]
        [Required]
        public int QuestionId { get; set; }

    }
}
