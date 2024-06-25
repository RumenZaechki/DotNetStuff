using ChainOfIrresponsibility.Abstractions;
using ChainOfIrresponsibility.Configuration.Extensions;
using ChainOfIrresponsibility.Tests.TestChain;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;

namespace ChainOfIrresponsibility.Tests
{
    public class ServiceCollectionTests
    {
        private readonly IServiceCollection _services;
        public ServiceCollectionTests()
        {
            _services = new ServiceCollection();

            _services
                .AddChain<IChain>()
                .WithLink<TestSuccessor>()
                .WithLink<AnotherTestSuccessor>();
        }

        [Fact]
        public void AddChain_Should_Add_Correct_Services()
        {
            _services
                .Should()
                .Contain(d => d.ServiceType == typeof(IChain));
        }

        [Fact]
        public void ChainBuilder_Should_Be_Added_To_Services()
        {
            _services
                .Should()
                .Contain(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IChainBuilder<IChain>));
        }

        [Fact]
        public void Chain_Should_Be_Constructed_Properly()
        {
            IChain chain = _services.BuildServiceProvider().GetRequiredService<IChain>();
            chain.Should().BeOfType<TestSuccessor>();
            chain.As<TestSuccessor>().Next.Should().BeOfType<AnotherTestSuccessor>();
        }
    }
}
