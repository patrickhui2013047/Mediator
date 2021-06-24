using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavior
{
    public delegate object ServiceResolver(Type type);
    public interface IMediator
    {
        void Register(object member);

        Task<TResponse>[] BoardcastAsync<TResponse>(IRequest<TResponse> request);

        Task<TResponse> SendAsync<TResponse>(int target, IRequest<TResponse> request);

        void Boardcast<TMessage>(TMessage message) where TMessage : IMessage;

        //void SetServiceResolver(ServiceResolver serviceResolver);
    }


}
