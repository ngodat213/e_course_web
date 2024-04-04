using e_course_web.Models;

namespace e_course_web.Repositorys
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Quiz> Quiz { get; }
        IRepository<QuizLesson> QuizLesson { get; }
        IRepository<QuizQuestion> QuizQuestion { get; }
        IRepository<Course> Course { get; }
        IRepository<CourseLesson> CourseLesson { get; }
        IRepository<CourseVideo> CourseVideo { get; }
        IRepository<Comment> Comment { get; }
        IRepository<Feedback> Feedback { get; }
        IRepository<Contact> Contact { get; }
        IRepository<Blog> Blog { get; }
        IRepository<Categories> Categories { get; }
        IRepository<User> User { get; }

    }
}
