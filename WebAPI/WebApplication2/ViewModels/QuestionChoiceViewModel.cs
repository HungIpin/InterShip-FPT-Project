using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class QuestionChoiceViewModel
    {
        public List<QuestionChoice> QuestionChoices { get; set; }
        public List<Question> Questions { get; set; }
    }
}
