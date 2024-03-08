using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ECourse.Models
{
    public class CourseVideo { 
        public CourseVideo(string _id, string title, int hour, int minute, int part, string videoUrl, string description)
        {
            Id = _id;
            Title = title;
            Hour = hour;
            Minute = minute;
            Part = part;
            VideoUrl = videoUrl;
            Description = description;
            Selection = 0;
            Comments = new List<string>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Selection { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Part { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public List<string> Comments { get; set; }
    }
}