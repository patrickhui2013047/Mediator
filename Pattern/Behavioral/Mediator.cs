using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.Pattern.Behavioral
{
    public interface IMediator
    {
        List<IMediatorClient> Clients { get; }
        void Noify(object sender, dynamic arg);
    }

    public class MediatorBase :IMediator
    {
        public List<IMediatorClient> Clients { get; private set; }

        protected MediatorBase()
        {
            Clients = new List<IMediatorClient>();
        }

        public void Noify(object sender, dynamic arg)
        {
            
        }
    }
}
