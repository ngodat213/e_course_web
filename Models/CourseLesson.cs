using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class CourseLesson
    {
        public CourseLesson(string _id, string title, int lesson, List<string> videos)
        {
            Id = _id;
            Title = title;
            Lesson = lesson;
            Videos = videos;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Lesson { get; set; }
        public List<string> Videos { get; set; }
    }
}