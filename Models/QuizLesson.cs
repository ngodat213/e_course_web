using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class QuizLesson
    {
        public QuizLesson(string _id, string title, int hour, int minute, int second, string lesson, double point, List<string> questions)
        {
            Id = _id; // Assuming you want to generate a new Guid for each QuizLesson object
            Title = title;
            Hour = hour;
            Minute = minute;
            Second = second;
            Lesson = lesson;
            Point = point;
            Questions = questions;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public string Lesson { get; set; }
        public double Point { get; set; }
        public List<string> Questions { get; set; }
    }
}