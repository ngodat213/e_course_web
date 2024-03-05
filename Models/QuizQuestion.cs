using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class QuizQuestion
    {
        public QuizQuestion(string _id, string questions, int answer, List<string> option)
        {
            Id = _id; // Assuming you want to generate a new Guid for each QuizQuestion object
            Questions = questions;
            Answer = answer;
            Option = option;
        }

        public string Id { get; set; }
        public string Questions { get; set; }
        public int Answer { get; set; }
        public List<string> Option { get; set; }
    }
}