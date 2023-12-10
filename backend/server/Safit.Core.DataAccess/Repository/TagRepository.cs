using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext context) : base(context) { }
}