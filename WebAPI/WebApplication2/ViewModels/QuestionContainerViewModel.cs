using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class QuestionContainerViewModel
    {
        public List<Question> Questions { get; set; }
        public List<QuestionSetting> QuestionSettings { get; set; }
    }
}
