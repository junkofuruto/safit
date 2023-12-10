using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context) { }
}