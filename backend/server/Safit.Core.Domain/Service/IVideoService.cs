using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface IVideoService
{
    public Task<Video> CreateAsync(AuthentificationToken token, string[] tags, long sportId, long? courseId, CancellationToken ct = default);
    public Task<Video> GetRecommendedAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<Video> GetInfoByIdAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<ReadOnlyMemory<byte>> GetContentAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<int> ToggleLikeAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<Comment> CreateCommentAsync(AuthentificationToken token, long videoId, long? branchId, string text, CancellationToken ct = default);
    public Task<int> GetLikesAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<IEnumerable<Comment>> GetCommentsAsync(AuthentificationToken token, long videoId, int count, int offset, long? originId, CancellationToken ct = default);
    public Task<Video> UploadAsync(AuthentificationToken token, long videoId, ReadOnlyMemory<byte> data, CancellationToken ct = default);
}