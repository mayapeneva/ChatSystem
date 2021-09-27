namespace ChatSystem.Infrastructure.Contracts
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessenger
    {
        Task PublishDirectAsync<T>(string exchange, string queue, string key, T message, CancellationToken cancellationToken);

        IAsyncEnumerable<T> GetDirectAsync<T>(string exchange, string queue, string key, CancellationToken cancellationToken);
    }
}
