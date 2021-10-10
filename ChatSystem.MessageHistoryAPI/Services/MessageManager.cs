namespace ChatSystem.MessageHistoryAPI.Services
{
    using ChatSystem.Infrastructure.Common;
    using Contracts;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using Infrastructure.Validators;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MessageManager : IMessageManager
    {
        private readonly IMessageRepository repository;

        public MessageManager(IMessageRepository repository)
        {
            this.repository = repository;
        }

        public Result<IEnumerable<Message>> Get(CancellationToken cancellationToken, int count = 0)
        {
            try
            {
                var result = repository.Get(cancellationToken, count);
                if (result is null)
                {
                    return new Result<IEnumerable<Message>>(Constants.NoMessagesFound, InternalStatusCode.NotFound, Constants.NoMessagesFound);
                }

                return new Result<IEnumerable<Message>>(result);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Message>>(Constants.CouldNotLoadMessages, InternalStatusCode.InternalServerError, ex.Message);
            }
        }

        public Result<IEnumerable<Message>> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new TimeFrameValidator();
                var validationResult = validator.Validate(timeFrame);
                if (!validationResult.IsValid)
                {
                    return new Result<IEnumerable<Message>>(Constants.TimeFrameNotValid, InternalStatusCode.BadRequest, validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var result = repository.GetPerTimePeriod(timeFrame, cancellationToken);
                if (result is null)
                {
                    return new Result<IEnumerable<Message>>(Constants.NoMessagesFound, InternalStatusCode.NotFound, Constants.NoMessagesFound);
                }

                return new Result<IEnumerable<Message>>(result);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Message>>(Constants.CouldNotLoadMessages, InternalStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Message>>> SaveAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            try
            {
                await repository.InsertAsync(messages, cancellationToken);
                return new Result<IEnumerable<Message>>(null);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Message>>(Constants.CouldNotSaveMessages, InternalStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
