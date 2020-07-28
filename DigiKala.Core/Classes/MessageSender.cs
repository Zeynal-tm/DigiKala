using System;
using System.Collections.Generic;
using System.Text;
using Kavenegar;

namespace DigiKala.Core.Classes
{
    public class MessageSender
    {
        public void SMS(string to, string body)
        {
            var sender = "10004346";
            var receptor = to;
            var message = body;
            var api = new KavenegarApi("");

            api.Send(sender, receptor, message);
        }
    }
}
