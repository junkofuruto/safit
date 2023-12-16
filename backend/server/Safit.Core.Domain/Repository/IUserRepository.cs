using Safit.Core.Domain.Model;

namespace Safit.Core.Domain.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    public Task CreateSubscribtion(User user, Trainer trainer, CancellationToken ct = default);
    public Task RemoveSubscribtion(User user, Trainer trainer, CancellationToken ct = default);
}