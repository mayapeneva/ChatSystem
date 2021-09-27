namespace ChatSystem.Infrastructure.Contracts
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageRepository
    {
        IEnumerable<Message> Get(CancellationToken cancellationToken, int count = default);

        IEnumerable<Message> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken);

        Task InsertAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
    }
}
