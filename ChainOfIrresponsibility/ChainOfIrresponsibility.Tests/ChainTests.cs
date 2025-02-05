using ChainOfIrresponsibility.Tests.TestChain;
using FluentAssertions;

namespace ChainOfIrresponsibility.Tests
{
    public class ChainTests
    {
        private TestRequest _request = new TestRequest();

        [Fact]
        public async Task With_Link_Chain_Calls_Successors_In_Correct_Order()
        {
            IChain chain = new ChainBuilder<IChain>()
                                .WithLink<TestSuccessor>()
                                .WithLink<AnotherTestSuccessor>()
                                .Build();

            await chain.HandleAsync(_request, new CancellationToken());

            _request.Logs.Should().HaveElementAt(0, "logging from TestSuccessor");
            _request.Logs.Should().HaveElementAt(1, "logging from AnotherTestSuccessor");
        }

        [Fact]
        public async Task With_Links_Chain_Calls_Successors_In_Correct_Order()
        {
            IChain chain = new ChainBuilder<IChain>()
                                .WithLinks(new List<Type>() { typeof(TestSuccessor), typeof(AnotherTestSuccessor) })
                                .Build();

            await chain.HandleAsync(_request, new CancellationToken());

            _request.Logs.Should().HaveElementAt(0, "logging from TestSuccessor");
            _request.Logs.Should().HaveElementAt(1, "logging from AnotherTestSuccessor");
        }

        [Fact]
        public void Chain_Throws_InvalidOperationException_When_There_Are_Two_Links_With_The_Same_Successor()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                IChain chain = new ChainBuilder<IChain>()
                                .WithLink<TestSuccessor>()
                                .WithLink<TestSuccessor>()
                                .Build();
            });

            Assert.Equal($"Final link of type '{typeof(TestSuccessor)}' expects next link in constructor, but no link was provided.", ex.Message);
        }

        [Fact]
        public void Chain_Throws_InvalidOperationException_When_There_Is_A_Successor_With_More_Than_1_Constructor()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                IChain chain = new ChainBuilder<IChain>()
                                .WithLink<TwoConstructorsSuccessor>()
                                .Build();
            });

            Assert.Equal($"Multiple public constructors found for type '{typeof(TwoConstructorsSuccessor)}'.", ex.Message);
        }
    }
}