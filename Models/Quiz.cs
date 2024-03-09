using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Quiz
    {
        public Quiz(string _id, string description, string image, string title, string type, List<string> lessons)
        {
            Id = _id;
            Description = description;
            Image = image;
            Title = title;
            Type = type;
            Lessons = lessons;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public List<string> Lessons { get; set; }
    }
}