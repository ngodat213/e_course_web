﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_course_web.Models
{
    public class Contact
    {
        public Contact() { }
        public Contact(string id, string fullName, string mail, string text, string topic)
        {
            Id = id;
            FullName = fullName;
            Mail = mail;
            Text = text;
            Topic = topic;
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Text { get; set; }
        public string Topic { get; set; }
    }
    public class ContactsResponse
    {
        public int count { get; set; }
        public List<Contact> contacts { get; set; }
    }
}