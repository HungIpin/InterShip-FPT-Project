using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class QuestionAttachmentViewModel
    {
        public List<QuestionAttachment> QuestionAttachments { get; set; }
        public List<Question> Questions { get; set; }
    }
}
