﻿using e_course_web.Models;

namespace e_course_web.ViewModels
{
    public class ExamLessonVM
    {
        public Exam Exam {  get; set; }
        public IFormFile Image { get; set; }

        public string Title { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Lesson { get; set; }
        public double Point { get; set; }
    }
}
