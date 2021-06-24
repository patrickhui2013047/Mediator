using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavioral
{
    public class MediatorMessage
    {
        public Type MessageType { get; private set; }
        public object Message { get; private set; }

        public MediatorMessage(object message)
        {
            Message = message;
            MessageType = message.GetType();
        }
    }
}
