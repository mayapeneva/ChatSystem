namespace ChatSystem.MessageHistoryAPI.Services
{
    using ChatSystem.Infrastructure.Common;
    using Contracts;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using Infrastructure.Validators;
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

        public IEnumerable<Message> Get(CancellationToken cancellationToken, int count = 0)
        {
            return repository.Get(cancellationToken, count);
        }

        public InternalResult<IEnumerable<Message>> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken)
        {
            if (true)
            {
                var validator = new TimeFrameValidator();
                var result = validator.Validate(timeFrame);
                if (!result.IsValid)
                {
                    return new InternalResult<IEnumerable<Message>>("", InternalStatusCode.BadRequest, result.Errors.Select(e => e.ErrorMessage));
                }
            }

            return new InternalResult<IEnumerable<Message>>(repository.GetPerTimePeriod(timeFrame, cancellationToken);
        }

        public async Task SaveAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            await repository.InsertAsync(messages, cancellationToken);
        }
    }
}
