using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface ITrainerService
{
    public Task<IQueryable<Video>> GetVideos(AuthentificationToken token, CancellationToken ct = default);
    public Task<IQueryable<Course>> GetCourses(AuthentificationToken token, CancellationToken ct = default);
    public Task<IQueryable<Post>> GetPosts(AuthentificationToken token, CancellationToken ct = default);
    public Task<IQueryable<Product>> GetProducts(AuthentificationToken token, CancellationToken ct = default);
    public Task<Sport> CreateSport(AuthentificationToken token, string name, string preview, string description, CancellationToken ct = default);
    public Task<IQueryable<Sport>> FindSportByName(string name, CancellationToken ct = default);
    public Task<bool> IsSpecialized(AuthentificationToken token, long sport, CancellationToken ct = default);
}