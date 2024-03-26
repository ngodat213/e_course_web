using e_course_web.Models;

namespace e_course_web.Repository
{
    public interface IUnitOfWork
    {
        /*
         * COURSE
         */
        IRepository<Course, CourseResponse> CourseRespo { get; }
        IRepository<CourseLesson, CourseLessonResponse> CourseLessonRespo { get; }
        IRepository<CourseVideo, CourseVideoResponse> CourseVideoRespo { get; }
        IRepository<CourseComment, CourseComment> CourseCommentRespo { get; } // fix
        /*
         * QUIZ
         */
        IRepository<Quiz, Quiz> QuizRespo { get; }// fix
        IRepository<QuizLesson, QuizLesson> QuizLessonRespo { get; }// fix
        IRepository<QuizQuestion, QuizQuestion> QuizQuestionRespo { get; }// fix
        /*
         * USER
         */
        IRepository<Contact, ContactResponse> ContactRespo { get; }
        IRepository<Blog, Blog> BlogRespo { get; }// fix
        IRepository<User, UserResponse> UserRespo { get; }
    }
}
