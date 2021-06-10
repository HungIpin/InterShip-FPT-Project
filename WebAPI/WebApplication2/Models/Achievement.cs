﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }// => byte[]
        public float AchievedPoints { get; set; }
        public virtual ICollection<AccountAchievement> AccountAchievements { get; set; }
    }
}