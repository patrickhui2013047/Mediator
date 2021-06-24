using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavior
{
    public interface ISubscriber<TMessage> where TMessage:IMessage
    {
        void Recieve(TMessage message);
    }
}
