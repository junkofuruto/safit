using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext context) : base(context) { }

    public async override Task Create(Tag entity, CancellationToken ct = default)
    {
        var tag = context.Tags.Where(x => x.Name == entity.Name).FirstOrDefault();
        if (tag != null)
        {
            entity = tag;
            return;
        }
        await base.Create(entity, ct);
    }
}