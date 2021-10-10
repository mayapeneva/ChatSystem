namespace ChatSystem.MessageHistoryAPI.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageManager
    {
        Result<IEnumerable<Message>> Get(CancellationToken cancellationToken, int count = default);

        Result<IEnumerable<Message>> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken);

        Task<Result<IEnumerable<Message>>> SaveAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
    }
}
