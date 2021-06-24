using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavior
{
    public interface IServiceProvider<in TRequest,TResponse>:IMember where TRequest :IRequest<TResponse>
    {
        
        Type RequestType => typeof(IRequest<TResponse>);
        bool IMember.IsServiceProvider => true;
        Task<TResponse> HandleAsync(TRequest request);
    }
}
