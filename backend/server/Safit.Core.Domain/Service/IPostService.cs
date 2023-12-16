﻿using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface IPostService
{
    public Task<Post> GetRecommendedAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<Post> GetById(AuthentificationToken token, long postId, CancellationToken ct = default);
}
