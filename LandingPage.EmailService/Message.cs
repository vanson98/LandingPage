using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandingPage.EmailService
{
    public class Message
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(string subject, string content)
        {
            Subject = subject;
            Content = content;
        }
    }
}
