using e_course_web.Models;

namespace e_course_web.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            CourseRespo = new Repository<Course, CourseResponse>();
            CourseLessonRespo = new Repository<CourseLesson, CourseLessonResponse>();
            CourseVideoRespo = new Repository<CourseVideo, CourseVideoResponse>();
            CourseCommentRespo = new Repository<CourseComment, CourseComment>();
            QuizRespo = new Repository<Quiz, QuizResponse>();
            QuizLessonRespo = new Repository<QuizLesson, QuizLesson>();
            QuizQuestionRespo = new Repository<QuizQuestion, QuizQuestion>();
            ContactRespo = new Repository<Contact, ContactResponse>();
            BlogRespo = new Repository<Blog, BlogResponse>();
            UserRespo = new Repository<User, UserResponse>();
        }

        public IRepository<Course, CourseResponse> CourseRespo { get; private set; }

        public IRepository<CourseLesson, CourseLessonResponse> CourseLessonRespo { get; private set; }

        public IRepository<CourseVideo, CourseVideoResponse> CourseVideoRespo { get; private set; }

        public IRepository<CourseComment, CourseComment> CourseCommentRespo { get; private set; }

        public IRepository<Quiz, QuizResponse> QuizRespo { get; private set; }

        public IRepository<QuizLesson, QuizLesson> QuizLessonRespo { get; private set; }

        public IRepository<QuizQuestion, QuizQuestion> QuizQuestionRespo { get; private set; }

        public IRepository<Contact, ContactResponse> ContactRespo { get; private set; }

        public IRepository<Blog, BlogResponse> BlogRespo { get; private set; }

        public IRepository<User, UserResponse> UserRespo { get; private set; }
    }
}
