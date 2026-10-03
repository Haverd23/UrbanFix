using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanFix.Core.Mediator
{
    public interface IMediator
    {
        Task<TResult> SendAsync<TRequest, TResult>(TRequest request)
               where TRequest : IRequest<TResult>;
    }
}
