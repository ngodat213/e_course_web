using e_course_web.Models;
using e_course_web.Service.Helpers;
using System.Collections.Generic;
using System.Text;

namespace e_course_web.ViewModels
{
    public class CourseDetailVM
    {
        public bool IsRegister {  get; set; }
        public Course Course { get; set; }
        public InstructorVM Instructor { get; set; }
        public IEnumerable<CourseFeedback> CourseFeedbacks { get; set; }
        public List<FeedbackVM> FeedbackVMs { get; set; }

        public List<double> RatingPercent {  get; set; }

        public List<FeedbackVM> GetFeedbackView()
        {
            List<FeedbackVM> list = new List<FeedbackVM>();
            foreach (var item in CourseFeedbacks)
            {
                list.Add(
                    new FeedbackVM
                    {
                        FullName = item.User.FullName,
                        PhotoUrl = item.User.PhotoUrl,
                        Rating = item.Rating,
                        Title = item.Title
                    }
                );
            }
            return list;
        }
    }
}
