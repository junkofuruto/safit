using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class VideoRepository : BaseRepository<Video>, IVideoRepository
{
    public VideoRepository(DatabaseContext context) : base(context) { }
}