using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class QuestionViewModel
    {
        public List<Question> Questions { get; set; }
        public List<QuestionType> QuestionTypes { get; set; }
        public List<SelectionSetting> SelectionSettings { get; set; }
        public List<QuestionPool> QuestionPools { get; set; }
        public List<QuestionSetting> QuestionSettings { get; set; }
    }
}
