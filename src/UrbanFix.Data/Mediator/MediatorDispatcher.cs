using Microsoft.Extensions.DependencyInjection;
using UrbanFix.Core.Mediator;

namespace UrbanFix.Data.Mediator
{
    public class MediatorDispatcher : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public MediatorDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
        {
            var handler = _serviceProvider
                .GetRequiredService<IRequestHandler<TRequest, TResult>>();

            return await handler.HandleAsync(request);
        }
    }
}