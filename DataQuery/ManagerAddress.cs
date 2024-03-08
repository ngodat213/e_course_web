namespace e_course_web.DataQuery
{
    public class ManagerAddress
    {
        private ManagerAddress() { }
        // Load env
        // ?--- Domain
        public static string domain = "http://localhost:3000/";
        // ?--- Course
        public static string course = "course";
        public static string courseLesson = "courseLesson";
        public static string courseVideo = "courseVideo";
        public static string courseComment = "courseComment";
        // ?--- Quiz
        public static string quiz = "quiz";
        public static string quizLesson = "quizLesson";
        public static string quizQuestion = "quizQuestion";
        // ?--- User
        public static string user = "user";
        public static string userLogin = "userLogin";
        public static string userSignUp = "signUp";
        public static string contact = "contact";
    }
}
