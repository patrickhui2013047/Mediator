using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavior
{
    
    public class Mediator:IMediator
    {
        //private ServiceResolver _serviceResolver;

        public List<IMember> Members = new List<IMember>();
        //IRequest=>key, provider list=>value
        public Dictionary<Type, List<object>> ServiceProviders = new Dictionary<Type, List<object>>();
        //IMessage=>key, subscriber list=>value
        public Dictionary<Type, List<object>> MessageSubscribers = new Dictionary<Type, List<object>>();

        public Mediator()
        {
            //_serviceResolver = serviceResolver;
        }

        public void Register(object member)
        {
            var info = member.GetType();
            if (info.GetInterfaces().Contains(typeof(IMember)))
            {
                var Member = (IMember)member;
                if (Member.IsServiceProvider&& info.GetInterfaces().Select(item=> {
                    if (item.ContainsGenericParameters)
                    {
                        return item.GetGenericTypeDefinition();
                    }
                    else
                    {
                        return item;
                    }
                }).Contains(typeof(IServiceProvider<,>)))
                {
                    var serviceList = info.GetInterfaces().Cast<Type>().ToList().FindAll(item => item.IsGenericType && item.GetGenericTypeDefinition() == typeof(IServiceProvider<,>));
                    foreach(var service in serviceList)
                    {
                        var request = service.GenericTypeArguments.First();
                        if (!ServiceProviders.ContainsKey(request))
                        {
                            ServiceProviders.Add(request, new List<object>());
                        }
                        ServiceProviders[request].Add(member);
                    }
                }

                if (Member.IsServiceProvider && info.GetInterfaces().Select(item => item.GetGenericTypeDefinition()).Contains(typeof(ISubscriber<>)))
                {
                    var subscriptionList = info.GetInterfaces().Cast<Type>().ToList().FindAll(item => item.IsGenericType && item.GetGenericTypeDefinition() == typeof(ISubscriber<>));
                    foreach (var subscription in subscriptionList)
                    {
                        var message = subscription.GenericTypeArguments.First();
                        if (!MessageSubscribers.ContainsKey(message))
                        {
                            MessageSubscribers.Add(message, new List<object>());
                        }
                        MessageSubscribers[message].Add(member);
                    }
                }

                Members.Add(Member);
                Member.SetID(Members.Max(item=>item.ID)+1);
            }
        }

        public Task<TResponse>[] BoardcastAsync<TResponse>(IRequest<TResponse> request)
        {
            var service = typeof(IRequest<TResponse>);
            if (ServiceProviders.ContainsKey(service))
            {
                List<Task<TResponse>> results = new List<Task<TResponse>>();
                foreach(IServiceProvider<IRequest<TResponse>,TResponse> serviceProvider in ServiceProviders[service])
                {
                    results.Add(serviceProvider.HandleAsync(request));
                }
                return results.ToArray();
            }
            return new Task<TResponse>[0];
        }

        public Task<TResponse> SendAsync<TResponse>(int target, IRequest<TResponse> request)
        {
            var service = typeof(IRequest<TResponse>);
            if (ServiceProviders.ContainsKey(service))
            {

                var serviceProvider = (IServiceProvider<IRequest<TResponse>, TResponse>)ServiceProviders[service].Find(item => ((IMember)item).ID == target);
                {
                    return serviceProvider.HandleAsync(request);
                }
                
            }
            return null;
        }

        public void Boardcast<TMessage>(TMessage message) where TMessage : IMessage
        {
            var subscribers = MessageSubscribers[message.GetType()];
            foreach(ISubscriber<TMessage> subscriber in subscribers)
            {
                subscriber.Recieve(message);
            }
        }

        

        
       
    }
}
