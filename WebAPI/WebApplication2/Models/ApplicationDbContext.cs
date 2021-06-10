using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillinCertification> SkillinCertifications { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamScore> ExamScores { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionChoice> QuestionChoices { get; set; }
        public DbSet<QuestionsInPart> QuestionsInParts { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<AccountAchievement> AccountAchievements { get; set; }
        public DbSet<AccountCertification> AccountCertifications { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<AccountCV> AccountCVs { get; set; }
        public DbSet<ExamHistory> ExamHistories { get; set; }
        public DbSet<FeedbackLevel> FeedbackLevels { get; set; }
        public DbSet<FeedbackType> FeedbackTypes { get; set; }
        public DbSet<QuestionPool> QuestionPools { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<ExamPart> ExamParts { get; set; }
        public DbSet<ExamHistoryDetail> ExamHistoryDetails { get; set; }
        public DbSet<SelectionSetting> SelectionSettings { get; set; }
        public DbSet<ExamSetting> ExamSettings { get; set; }
        public DbSet<ScoreRecording> ScoreRecordings { get; set; }
        public DbSet<AdvancedFeedback> AdvancedFeedbacks { get; set; }
        public DbSet<AdvancedFeedbacksInExam> AdvancedFeedbacksInExams { get; set; }
        public DbSet<QuestionAttachment> QuestionAttachment { get; set; }
        public DbSet<QuestionSetting> QuestionSetting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(a => a.User).WithOne(u => u.Account).HasForeignKey<User>(u => u.AccountId).OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<Template>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<AccountCV>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.TemplateId });
                entity.HasOne(e => e.Account).WithMany(a => a.AccountCVs).HasForeignKey(a => a.AccountId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Template).WithMany(d => d.AccountCVs).HasForeignKey(d => d.TemplateId).OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<SkillinCertification>(entity =>
            {
                entity.HasKey(e => new { e.SkillId, e.CertificationId });
                entity.HasOne(e => e.Certification).WithMany(d => d.SkillinCertifications).HasForeignKey(d => d.CertificationId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Skill).WithMany(d => d.SkillinCertifications).HasForeignKey(d => d.SkillId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Exam>(entity =>
            { 
                entity.HasOne(a => a.ExamSetting).WithOne(u => u.Exam).HasForeignKey<ExamSetting>(u => u.ExamId);
                entity.HasOne(e => e.Account).WithMany(a => a.Exams).HasForeignKey(a => a.AccountId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Certification).WithMany(d => d.Exams).HasForeignKey(d => d.CertificationId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.FeedbackType).WithMany(d => d.Exams).HasForeignKey(d => d.FeedbackTypeId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.FeedbackLevel).WithMany(d => d.Exams).HasForeignKey(d => d.FeedbackLevelId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.ScoreRecording).WithMany(d => d.Exams).HasForeignKey(d => d.ScoreRecordingId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<FeedbackType>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<FeedbackLevel>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<AccountAchievement>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.AchievementId });
                entity.HasOne(e => e.Account).WithMany(a => a.AccountAchievements).HasForeignKey(a => a.AccountId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Achievement).WithMany(a => a.AccountAchievements).HasForeignKey(a => a.AchievementId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<AccountCertification>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.CertificationId });
                entity.HasOne(e => e.Account).WithMany(a => a.AccountCertifications).HasForeignKey(a => a.AccountId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Certification).WithMany(d => d.AccountCertifications).HasForeignKey(d => d.CertificationId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(a => a.QuestionSetting).WithOne(u => u.Question).HasForeignKey<QuestionSetting>(u => u.QuestionId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.SelectionSetting).WithMany(u => u.Questions).HasForeignKey(u => u.SelectionSettingId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.QuestionType).WithMany(u => u.Questions).HasForeignKey(u => u.QuestionTypeId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.QuestionPool).WithMany(u => u.Questions).HasForeignKey(u => u.QuestionPoolId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<QuestionChoice>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(a => a.Question).WithMany(u => u.QuestionChoices).HasForeignKey(u => u.QuestionId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<QuestionAttachment>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(a => a.Question).WithMany(u => u.QuestionAttachments).HasForeignKey(u => u.QuestionId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<QuestionPool>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.ParentPool).WithMany(d => d.ChildQuestionPools).HasForeignKey(d => d.ParentPoolId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Account).WithMany(a => a.QuestionPools).HasForeignKey(a => a.AccountId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<QuestionsInPart>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.Question).WithMany(d => d.QuestionsInParts).HasForeignKey(d => d.QuestionId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.ExamPart).WithMany(d => d.QuestionsInParts).HasForeignKey(d => d.ExamPartId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<QuestionSetting>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<ExamPart>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.Exam).WithMany(d => d.ExamParts).HasForeignKey(d => d.ExamId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.QuestionPool).WithMany(d => d.ExamParts).HasForeignKey(d => d.QuestionPoolId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ExamHistory>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.Account).WithMany(a => a.ExamHistories).HasForeignKey(a => a.AccountId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.QuestionsInPart).WithMany(d => d.ExamHistories).HasForeignKey(d => d.QuestionsInPartId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ExamHistoryDetail>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.ExamHistory).WithMany(a => a.ExamHistoryDetails).HasForeignKey(a => a.ExamHistoryId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<SelectionSetting>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<ScoreRecording>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<AdvancedFeedback>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();

            });
            modelBuilder.Entity<ExamScore>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasOne(e => e.Exam).WithMany(d => d.ExamScores).HasForeignKey(d => d.ExamId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Account).WithMany(e => e.ExamScores).HasForeignKey(e => e.AccountId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<AdvancedFeedbacksInExam>(entity =>
            {
                entity.HasKey(e => new { e.ExamId, e.AdvancedFeedbackId });
                entity.HasOne(e => e.Exam).WithMany(d => d.AdvancedFeedbacksInExams).HasForeignKey(d => d.ExamId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.AdvancedFeedback).WithMany(d => d.AdvancedFeedbacksInExams).HasForeignKey(d => d.AdvancedFeedbackId).OnDelete(DeleteBehavior.Restrict);
            });
        }
        
       
    }
}
