using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class QuestionPool
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        [Required]
        public int AccountId { get; set; }
        public QuestionPool ParentPool { get; set; }
        public int? ParentPoolId { get; set; }
        public ICollection<QuestionPool> ChildQuestionPools { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<ExamPart> ExamParts { get; set; }

    }
}
