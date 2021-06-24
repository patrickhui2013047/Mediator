using System;
using System.Threading.Tasks;
using PH.Pattern.Behavior;
namespace Sample
{
    class Program
    {
        static Mediator Mediator = new Mediator();
        static TestMember Member1 = new TestMember();
        static TestMember Member2 = new TestMember();
        static void Main(string[] args)
        {
            Mediator.Register(Member1);
            Mediator.Register(Member2);

            Mediator.SendAsync<ProcessDoneResponse>(((IMember)Member1).ID, new IRequest<ProcessDoneResponse>());
            Mediator.SendAsync<ProcessDoneResponse>(((IMember)Member2).ID, new IRequest<ProcessDoneResponse>());

            Mediator.BoardcastAsync<ProcessDoneResponse>(new IRequest<ProcessDoneResponse>());
            Mediator.Boardcast<TestMessage>(new TestMessage());

        }
    }

    class TestMember : IServiceProvider<TestRequest, ProcessDoneResponse>, ISubscriber<TestMessage>
    {
        public Type MemberType => typeof(TestMember);

        int IMember.ID { get; set; }

        public Task<ProcessDoneResponse> HandleAsync(TestRequest request)
        {
            return new Task<ProcessDoneResponse>(Handle);
        }

        public void Recieve(TestMessage message)
        {
            return;
        }

        private ProcessDoneResponse Handle()
        {
            return new ProcessDoneResponse();
        }
    }
    class TestRequest : IRequest<ProcessDoneResponse>
    {

    }
    class TestMessage : IMessage { }

}
