using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(DatabaseContext context) : base(context) { }
}