using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECourse.Models
{
    public class Course
    {
        public Course(string _id, string title, double price, string category, string description, string rating, int order, string teacherId, string courseImage, string time, List<string> lessons)
        {
            Id = _id;
            Title = title;
            Price = price;
            Category = category;
            Description = description;
            Rating = rating;
            Order = order;
            TeacherId = teacherId;
            CourseImage = courseImage;
            Time = time;
            Lessons = lessons;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public int Order { get; set; }
        public string TeacherId { get; set; }
        public string CourseImage { get; set; }
        public string Time { get; set; }
        public List<string> Lessons { get; set; }
    }
}