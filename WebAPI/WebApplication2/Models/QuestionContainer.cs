using System;

namespace WebApplication2.Models
{
    public class QuestionContainer
    {


        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionPoolId { get; set; }
        public int SelectionSettingId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CorrectFb { get; set; }
        public string InCorrectFb { get; set; }
        public string Choice { get; set; }
        public bool DisplayPoint { get; set; }
        public float PointValue { get; set; }
        public string Answer { get; set; }
    }
}
