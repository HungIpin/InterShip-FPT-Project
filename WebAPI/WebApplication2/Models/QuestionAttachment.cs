using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class QuestionAttachment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Attachment { get; set; }
        public Question Question { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}
