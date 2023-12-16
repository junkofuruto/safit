using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

namespace Safit.Core.Domain.Service;

public interface IContentService
{
    public Task<Course> PurchaseCourseAsync(AuthentificationToken token, long courseId, CancellationToken ct = default);
    public Task<Course> GetCourseInfoAsync(long courseId, CancellationToken ct = default);
    public Task<CourseContent> GetCourseContentAsync(long courseId, CancellationToken ct = default);
}