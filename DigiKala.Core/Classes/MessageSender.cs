using System;
using System.Collections.Generic;
using System.Text;
using Kavenegar;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace DigiKala.Core.Classes
{
    public class MessageSender
    {
        public void SMS(string to, string body)
        {
            var sender = "10004346";
            var receptor = to;
            var message = body;
            var api = new KavenegarApi("752F4D42483276764C67675977545032684C496B41384A78585A4350632B7963");

            api.Send(sender, receptor, message);
        }

        public static void Email(string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com");

            message.From = new MailAddress("", "دیجی کالا");
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            message.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("", "");
            client.Send(message);

            

            
        }
    }
}
