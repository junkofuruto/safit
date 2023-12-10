using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(DatabaseContext context) : base(context) { }
}