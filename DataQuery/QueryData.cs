using e_course_web.Manager;
using e_course_web.Models;
using e_course_web.Repository;

namespace e_course_web.DataQuery
{
    public static class QueryData
    {

        private static readonly IRepository<CourseLesson, CoursesResponse> _courseLessonRepository = new Repository<CourseLesson, CoursesResponse>();
        private static readonly IRepository<QuizLesson, CoursesResponse> _quizLessonRepository = new Repository<QuizLesson, CoursesResponse>();
        private static readonly IRepository<QuizQuestion, CoursesResponse> _quizQuestionRepository = new Repository<QuizQuestion, CoursesResponse>();
        //--------------------------FUNC-----------------------

        /*  QUIZ FUNCTION
         *  getListQuizLesson(Quiz model)
         *  getListQuizQuestion(QuizLesson model)
         */
        /*public static async Task<List<QuizLesson>> getListQuizLesson(Quiz model)
        {
            List<QuizLesson> res = new List<QuizLesson>();
            foreach (var id in model.Lessons)
            {
                res.Add(await _quizLessonRepository.GetAsync(id, ManagerAddress.domain, ManagerAddress.quizLesson));
            }
            return res;
        }*/

        /*public static async Task<List<QuizQuestion>> getListQuizQuestion(QuizLesson model)
        {
            List<QuizQuestion> res = new List<QuizQuestion>();
            foreach (var id in model.Questions)
            {
                res.Add(await _quizLessonRepository.quizQuestionGetById(id));
            }
            return res;
        }

        *//*  COURSE FUNCTION
         *  getListQuizLesson(Quiz model)
         *  getListQuizQuestion(QuizLesson model)
         *//*
        public static async Task<List<CourseLesson>> getListCourseLesson(Course model)
        {
            List<CourseLesson> res = new List<CourseLesson>();
            foreach (var id in model.Lessons)
            {
                res.Add(await QueryDataAPI.courseLessonGetById(id));
            }
            return res;
        }

        public static async Task<List<CourseVideo>> getListCourseVideo(CourseLesson model)
        {
            List<CourseVideo> res = new List<CourseVideo>();
            foreach (var id in model.Videos)
            {
                res.Add(await QueryDataAPI.courseVideoGetById(id));
            }
            return res;
        }*/
    }
}
