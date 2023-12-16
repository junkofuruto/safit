using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface IVideoService
{
    public Task<Video> GetRecommendedAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<Video> GetInfoByIdAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<ReadOnlyMemory<byte>> GetContentAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<int> ToggleLikeAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<Comment> CreateCommentAsync(AuthentificationToken token, long videoId, long branchId, string text, CancellationToken ct = default);
    public Task<int> GetLikesAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task<Comment> GetCommentsAsync(AuthentificationToken token, long videoId, int count = 20, int offset = 0, CancellationToken ct = default);
    public Task<Video> UploadAsync(AuthentificationToken token, long videoId, ReadOnlySpan<byte> data);
}