using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class AccountCertification
    {
        public Certification Certification { get; set; }
        public string CertificationId { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public DateTime AchievedDate { get; set; }
    }
}
