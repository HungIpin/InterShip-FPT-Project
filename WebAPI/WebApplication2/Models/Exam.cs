using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Exam
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumOfQuestions { get; set; }
        public float PassScore { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Rating { get; set; }
        public Certification Certification { get; set; }
        [Required]
        public string CertificationId { get; set; }
        public FeedbackType FeedbackType { get; set; }
        [Required]
        public int FeedbackTypeId { get; set; }
        public FeedbackLevel FeedbackLevel { get; set; }
        [Required]
        public int FeedbackLevelId { get; set; }
        public Account Account { get; set; }
        [Required]
        public int AccountId { get; set; }
        public ScoreRecording ScoreRecording { get; set; }
        [Required]
        public int ScoreRecordingId { get; set; }
        public ICollection<ExamScore> ExamScores { get; set; }
        public ICollection<ExamPart> ExamParts { get; set; }
        public ICollection<AdvancedFeedbacksInExam> AdvancedFeedbacksInExams { get; set; }
        public ExamSetting ExamSetting { get; set; }
    }
}
