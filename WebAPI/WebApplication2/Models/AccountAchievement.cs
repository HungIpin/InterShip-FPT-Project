using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class AccountAchievement
    {
        public Achievement Achievement { get; set; }
        public int AchievementId { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}
