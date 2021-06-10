using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class ExamSettingViewModel
    {
        public List<ExamSetting> ExamSettings { get; set; }
        public List<Exam> Exams { get; set; }
    }
}
