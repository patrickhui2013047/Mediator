using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavioral
{
    public interface IMediatorClient
    {
        IMediator Mediator { get; protected set; }

        void SetMediator(IMediator mediator);
    }

    public abstract class MediatorClientBase
    {
        public IMediator Mediator { get; protected set; }


        public MediatorClientBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
