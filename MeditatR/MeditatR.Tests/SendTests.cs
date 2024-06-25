using FluentAssertions;
using MeditatR.Tests.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace MeditatR.Tests
{
    public class SendTests
    {
        private readonly IServiceCollection _services;
        private readonly IMediator _mediator;

        public SendTests()
        {
            _services = new ServiceCollection();
            _services.AddMeditatR("MeditatR.Tests");
            var provider = _services.BuildServiceProvider();
            _mediator = provider.GetService<IMediator>();
        }

        [Fact]
        public async Task Send_Should_Resolve_Handler()
        {
            var response = await _mediator.SendAsync<Ping, Pong>(new Ping { Message = "Ping" }, new CancellationToken());

            response.Message
                .Should()
                .Be("Ping Pong");
        }

        [Fact]
        public async Task Send_Event_Should_Resolve_Handler()
        {
            var ping = new Ping();
            await _mediator.SendAsync<Ping>(ping, new CancellationToken());

            ping.Message
                .Should()
                .Be("Ping Pong Ping");
        }

        [Fact]
        public async Task Send_Should_Throw_InvalidOperationException_When_Handler_Is_Not_Resolved()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
               await _mediator.SendAsync<string, Pong>("asdf", new CancellationToken());
            });

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _mediator.SendAsync<string>("asdf", new CancellationToken());
            });
        }
    }
}
