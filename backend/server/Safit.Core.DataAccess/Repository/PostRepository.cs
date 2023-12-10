using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(DatabaseContext context) : base(context) { }
}