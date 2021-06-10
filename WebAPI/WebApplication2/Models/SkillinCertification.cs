using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class SkillinCertification
    {
        public Certification Certification { get; set; }
        [Required]
        public string CertificationId { get; set; }
        public Skill Skill { get; set; }
        [Required]
        public string SkillId { get; set; }
    }
}
