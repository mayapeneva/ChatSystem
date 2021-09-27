namespace ChatSystem.MessageHistoryAPI.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageManager
    {
        IEnumerable<Message> Get(CancellationToken cancellationToken, int count = default);

        InternalResult<IEnumerable<Message>> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken);

        Task SaveAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
    }
}
