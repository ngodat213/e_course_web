using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class CourseComment
    {
        public CourseComment(string _id, string title, int like = 0, List<string> userId = null)
        {
            Id = _id;
            Title = title;
            Like = like;
            UserId = userId ?? new List<string>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Like { get; set; }
        public List<string> UserId { get; set; }
    }
}