using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
