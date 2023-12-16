using Microsoft.EntityFrameworkCore;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Repository.Exceptions;

namespace Safit.Core.DataAccess.Repository;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context) { }

    public async Task CreateSubscribtion(User user, Trainer trainer, CancellationToken ct = default)
    {
        if (trainer == null || user == null) throw new ArgumentNullException();
        var selected = await context.Users.Where(x => x.Id == user.Id).FirstAsync();
        if (selected != null) selected.Trainers.Add(trainer);
    }

    public async Task RemoveSubscribtion(User user, Trainer trainer, CancellationToken ct = default)
    {
        if (trainer == null || user == null) throw new ArgumentNullException();
        var selectedUser = await context.Users.Where(x => x.Id == user.Id).FirstAsync();
        if (selectedUser != null) selectedUser.Trainers.Remove(trainer);
    }

    public async override Task Update(User entity, CancellationToken ct = default)
    {
        var exist = await context.Users.Where(i => i.Id == entity.Id).FirstOrDefaultAsync();
        if (exist == null) throw new EntityNullException();
        var entry = context.Entry(exist);
        entry.CurrentValues.SetValues(entity);
        entry.State = EntityState.Modified;
    }
}