namespace ChatSystem.Infrastructure.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessenger
    {
        Task SendAsync<T>(string exchange, string queue, string key, T message, CancellationToken cancellationToken);
    }
}
