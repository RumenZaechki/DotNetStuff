using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace MeditatR.Tests
{
    public class ServiceCollectionTests
    {
        private readonly IServiceCollection _services;
        private readonly IMediator _mediator;

        public ServiceCollectionTests()
        {
            _services = new ServiceCollection();
            _services.AddMeditatR("MeditatR.Tests");
            var provider = _services.BuildServiceProvider();
            _mediator = provider.GetService<IMediator>();
        }

        [Fact]
        public void Mediator_Should_Be_In_Services()
        {
            _services
                .Should()
                .Contain(m => m.ServiceType == typeof(IMediator));
        }
    }
}
