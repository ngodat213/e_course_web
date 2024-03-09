using e_course_web.Models;

namespace e_course_web.DataQuery
{
    public static class QueryData
    {
        //--------------------------FUNC-----------------------

        /*  QUIZ FUNCTION
         *  getListQuizLesson(Quiz model)
         *  getListQuizQuestion(QuizLesson model)
         */
        public static async Task<List<QuizLesson>> getListQuizLesson(Quiz model)
        {
            List<QuizLesson> res = new List<QuizLesson>();
            foreach (var id in model.Lessons)
            {
                res.Add(await QueryDataAPI.quizLessonGetById(id));
            }
            return res;
        }

        public static async Task<List<QuizQuestion>> getListQuizQuestion(QuizLesson model)
        {
            List<QuizQuestion> res = new List<QuizQuestion>();
            foreach (var id in model.Questions)
            {
                res.Add(await QueryDataAPI.quizQuestionGetById(id));
            }
            return res;
        }

        /*  COURSE FUNCTION
         *  getListQuizLesson(Quiz model)
         *  getListQuizQuestion(QuizLesson model)
         */
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
        }
    }
}
