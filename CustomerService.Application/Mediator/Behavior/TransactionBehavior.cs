using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.DataAccess.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerService.Application.Mediator.Behavior
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(
            IUnitOfWork unitOfWork,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                var response = await next();
                await _unitOfWork.Commit();

                return response;
            }
            catch(Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                throw;
            }
            finally
            {
                _unitOfWork.CloseTransaction();
            }
        }
    }
}