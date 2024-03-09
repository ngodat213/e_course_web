using e_course_web.Manager;
using e_course_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.DataQuery
{
    public static class QueryDataAPI
    {
        //------------------------API------------------------
        /*  User
         *  Request accept: Login, SignUp
         *  Function: Get bearer
         */
        public static async Task<string> userLogin()
        {
            UserResponse res = await APICall.RunAsyncGetAll<UserResponse>(ManagerAddress.user, ManagerAddress.login);
            return res.token;
        }

        public static async Task<string> userSignUp()
        {
            return await APICall.RunAsyncGetAll<string>(ManagerAddress.user, ManagerAddress.signUp);
        }

        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> contactGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.contact);
            return res;
        }
        public static async Task<Contact> contactGetById(string id)
        {
            Contact res = await APICall.RunAsyncGetAll<Contact>(ManagerAddress.domain, ManagerAddress.contact, id);
            return res;
        }
        public static async Task<bool> contactCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<CoursesResponse> courseGetAll()
        {
            CoursesResponse res = await APICall.RunAsyncGetAll<CoursesResponse>(ManagerAddress.domain, ManagerAddress.course);
            return res;
        }
        public static async Task<Contact> courseGetById(string id)
        {
            Contact res = await APICall.RunAsyncGetAll<Contact>(ManagerAddress.domain, ManagerAddress.contact, id);
            return res;
        }
        public static async Task<bool> courseCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> courseCommentGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.courseComment);
            return res;
        }
        public static async Task<Contact> courseCommentGetById(string id)
        {
            Contact res = await APICall.RunAsyncGetAll<Contact>(ManagerAddress.domain, ManagerAddress.contact, id);
            return res;
        }
        public static async Task<bool> courseCommentCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> courseLessonGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.courseLesson);
            return res;
        }
        public static async Task<CourseLesson> courseLessonGetById(string id)
        {
            CourseLesson res = await APICall.RunAsyncGetAll<CourseLesson>(ManagerAddress.domain, ManagerAddress.courseLesson, id);
            return res;
        }
        public static async Task<bool> courseLessonCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> courseVideoGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.courseVideo);
            return res;
        }
        public static async Task<CourseVideo> courseVideoGetById(string id)
        {
            CourseVideo res = await APICall.RunAsyncGetAll<CourseVideo>(ManagerAddress.domain, ManagerAddress.courseVideo, id);
            return res;
        }
        public static async Task<bool> courseVideoCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> quizGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.quiz);
            return res;
        }
        public static async Task<Contact> quizGetById(string id)
        {
            Contact res = await APICall.RunAsyncGetAll<Contact>(ManagerAddress.domain, ManagerAddress.contact, id);
            return res;
        }
        public static async Task<bool> quizCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> quizLessonGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.quizLesson);
            return res;
        }
        public static async Task<QuizLesson> quizLessonGetById(string id)
        {
            QuizLesson res = await APICall.RunAsyncGetAll<QuizLesson>(ManagerAddress.domain, ManagerAddress.contact, id);
            return res;
        }
        public static async Task<bool> quizLessonCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> quizQuestionGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.quizQuestion);
            return res;
        }
        public static async Task<QuizQuestion> quizQuestionGetById(string id)
        {
            QuizQuestion res = await APICall.RunAsyncGetAll<QuizQuestion>(ManagerAddress.domain, ManagerAddress.contact, id);
            return res;
        }
        public static async Task<bool> quizQuestionCreate(Contact obj)
        {
            return await APICall.RunAsyncCreate<Contact>(ManagerAddress.domain, ManagerAddress.contact, obj);
        }
    }
}
