using Microsoft.EntityFrameworkCore;
using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context) { }
}