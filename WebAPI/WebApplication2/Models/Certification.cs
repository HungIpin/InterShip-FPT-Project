using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Certification
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TakenTimes { get; set; }
        public byte[] Image { get; set; }
        public string Difficulty { get; set; }
        public ICollection<SkillinCertification> SkillinCertifications { get; set; }
        public ICollection<AccountCertification> AccountCertifications { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
