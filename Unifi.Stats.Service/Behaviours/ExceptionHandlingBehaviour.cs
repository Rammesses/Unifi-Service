using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Unifi.Stats.Service.Behaviours
{
    public class ExceptionHandlingBehaviour<TRequest, TResponse> : AsyncRequestExceptionHandler<TRequest, TResponse>
    {
        private readonly ILogger logger;

        public ExceptionHandlingBehaviour(ILogger<ExceptionHandlingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        protected override Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            this.logger.LogError(exception, exception?.Message);

            state?.SetHandled();

            return Task.CompletedTask;
        }
    }
}
