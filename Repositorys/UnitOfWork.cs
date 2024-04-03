using e_course_web.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing.Drawing2D;
using NuGet.Protocol.Plugins;

namespace e_course_web.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Course = new Repository<Course>(_db);
            CourseLesson = new Repository<CourseLesson>(_db);
            CourseVideo = new Repository<CourseVideo>(_db);
            Feedback = new Repository<Feedback>(_db);
            Quiz = new Repository<Quiz>(_db);
            QuizLesson = new Repository<QuizLesson>(_db);
            QuizQuestion = new Repository<QuizQuestion>(_db);
            Contact = new Repository<Contact>(_db);
            Blog = new Repository<Blog>(_db);
            User = new Repository<User>(_db);
        }

        public IRepository<Course> Course { get; private set; }

        public IRepository<CourseLesson> CourseLesson { get; private set; }

        public IRepository<CourseVideo> CourseVideo { get; private set; }

        public IRepository<Feedback> Feedback { get; private set; }

        public IRepository<Quiz> Quiz { get; private set; }

        public IRepository<QuizLesson> QuizLesson { get; private set; }

        public IRepository<QuizQuestion> QuizQuestion { get; private set; }

        public IRepository<Contact> Contact { get; private set; }

        public IRepository<Blog> Blog { get; private set; }

        public IRepository<User> User { get; private set; }
        public IRepository<Comment> Comment { get; private set; }
        public IRepository<Categories> Categories { get; private set; }
    }
}