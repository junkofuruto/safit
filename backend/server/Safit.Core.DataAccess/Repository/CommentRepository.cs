using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(DatabaseContext context) : base(context) { }
}