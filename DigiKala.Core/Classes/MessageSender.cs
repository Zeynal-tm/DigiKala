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
            var api = new KavenegarApi("752F4D42483276764C67675977545032684C496B41384A78585A4350632B7963");

            api.Send(sender, receptor, message);
        }
    }
}
