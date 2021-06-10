using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;


namespace WebApplication2.ViewModels
{
    public class QuestionPoolViewModel
    {
        public List<QuestionPool> QuestionPools { get; set; }
        public List<Account> Accounts { get; set; }

        public List<QuestionPool> QuestionPoolParents { get; set; }
    }
}
