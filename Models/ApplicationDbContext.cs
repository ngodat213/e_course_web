using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace e_course_web.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // COURSE
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        // QUIZ
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<QuizLesson> QuizLessons { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        // USER
        public DbSet<Blog> Blogs { get; set; }
        /*public DbSet<Comment> Comments { get; set; }*/
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
