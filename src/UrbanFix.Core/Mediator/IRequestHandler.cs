using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanFix.Core.Mediator
{
    public interface IRequestHandler<TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}
