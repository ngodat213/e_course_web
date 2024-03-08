using ECourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_course_web.DataQuery
{
    public static class QueryData
    {
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
        public static async Task<ContactsResponse> courseGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.course);
            return res;
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
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> courseLessonGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.courseLesson);
            return res;
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
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> quizGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.quiz);
            return res;
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
        /* Contact
         * Request accept: Post, Get
         * Request accept auth: Path, Delete
         */
        public static async Task<ContactsResponse> quizQuestionGetAll()
        {
            ContactsResponse res = await APICall.RunAsyncGetAll<ContactsResponse>(ManagerAddress.domain, ManagerAddress.quizQuestion);
            return res;
        }
    }
}
