using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ExamPart
    {
        [Key]
        public int Id { get; set; }
        public Exam Exam { get; set; }
        [Required]
        public string ExamId { get; set; }
        public string Name { get; set; }
        public int SequenceNo { get; set; }
        public QuestionPool QuestionPool { get; set; }
        public int? QuestionPoolId { get; set; }
        public int NumOfQuestions { get; set; }
        public float QuestionPoints { get; set; }
        public float DeductedPoints { get; set; }
        public bool IsShuffle { get; set; }
        public virtual ICollection<QuestionsInPart> QuestionsInParts { get; set; }

    }
}
