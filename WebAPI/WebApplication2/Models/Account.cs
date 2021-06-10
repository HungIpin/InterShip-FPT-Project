using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
        public ICollection<ExamScore> ExamScores { get; set; }
        public ICollection<AccountAchievement> AccountAchievements { get; set; }
        public ICollection<AccountCertification> AccountCertifications { get; set; }
        public ICollection<AccountCV> AccountCVs { get; set; }
        public ICollection<ExamHistory> ExamHistories { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<QuestionPool> QuestionPools { get; set; }
    }
}
