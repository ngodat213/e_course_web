﻿namespace e_course_web.Manager
{
    public static class ManagerAddress
    {
        // Load env
        // ?--- Domain
        public static string domain = "http://localhost:3000/";
        // ?--- Course
        public static string course = "course/";
        public static string courseLesson = "courseLesson/";
        public static string courseVideo = "courseVideo/";
        public static string courseComment = "courseComment/";
        // ?--- Quiz
        public static string quiz = "quiz/";
        public static string quizLesson = "quizLesson/";
        public static string quizQuestion = "quizQuestion/";
        // ?--- User
        public static string user = $"{domain}user/";
        public static string login = $"{user}login/";
        public static string signup = $"{user}signUp/";
        public static string contact = "contact";
        public static string blog = "blog";
        public static string qa = "qa";
    }
}
