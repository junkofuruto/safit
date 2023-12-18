using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface IRecommendationService
{
    public Task ViewVideoAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task LikeVideoAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task CommentVideoAsync(AuthentificationToken token, long videoId, CancellationToken ct = default);
    public Task ViewPostAsync(AuthentificationToken token, long postId, CancellationToken ct = default);
    public Task<Tag> GetRecommendedTag(AuthentificationToken token, CancellationToken ct = default);
}