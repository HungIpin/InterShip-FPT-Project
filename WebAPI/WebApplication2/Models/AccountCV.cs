using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class AccountCV
    {
        public Account Account { get; set; }
        [Required]
        public int AccountId { get; set; }
        public Template Template { get; set; }
        [Required]
        public int TemplateId { get; set; }

        public byte[] Attachment { get; set; }
    }
}
