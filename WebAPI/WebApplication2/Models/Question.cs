
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionText { get; set; }   //Nội dung câu hỏi
        public QuestionType QuestionType { get; set; }
        [Required]
        public int QuestionTypeId { get; set; }
        public SelectionSetting SelectionSetting { get; set; }
        public int SelectionSettingId { get; set; }
        public QuestionSetting QuestionSetting { get; set; }

        public QuestionPool QuestionPool { get; set; }
        public int? QuestionPoolId { get; set; }
        public ICollection<QuestionsInPart> QuestionsInParts { get; set; }
        public ICollection<QuestionAttachment> QuestionAttachments { get; set; }
        public ICollection<QuestionChoice> QuestionChoices { get; set; }

    }
}
