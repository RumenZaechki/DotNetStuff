using ChainOfIrresponsibility.Tests.TestChain;
using FluentAssertions;

namespace ChainOfIrresponsibility.Tests
{
    public class ChainTests
    {
        private TestRequest _request = new TestRequest();
        [Fact]
        public async void Chain_Calls_Successors_In_Correct_Order()
        {
            IChain chain = new ChainBuilder<IChain>()
                                .WithLink<TestSuccessor>()
                                .WithLink<AnotherTestSuccessor>()
                                .Build();

            await chain.HandleAsync(_request, new CancellationToken());

            _request.Logs.Should().HaveElementAt(0, "logging from TestSuccessor");
            _request.Logs.Should().HaveElementAt(1, "logging from AnotherTestSuccessor");
        }
    }
}