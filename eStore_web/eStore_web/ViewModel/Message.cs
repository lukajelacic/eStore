using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public enum MessageType { Primary=0,Secondary,Success,Danger,Warning,Info}
    public class Message
    {
        public MessageType MessageType { get; set; }
        public string MessageText { get; set; }

        public Message(MessageType rt,string mt)
        {
            MessageType = rt;
            MessageText = mt;
        }
    }
    public class Messages
    {
        public List<Message> messagesList { get; set; }


        public Messages()
        {
            messagesList = new List<Message>();
        }
       
    }
}
