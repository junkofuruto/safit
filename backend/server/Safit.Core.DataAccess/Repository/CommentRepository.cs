using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(DatabaseContext context) : base(context) { }
}