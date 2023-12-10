using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext context) : base(context) { }
}