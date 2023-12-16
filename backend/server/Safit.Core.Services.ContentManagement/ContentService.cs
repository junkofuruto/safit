using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

namespace Safit.Core.Services.ContentManagement;

public class ContentService : IContentService
{


    public Task<CourseContent> GetCourseContentAsync(long courseId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Course> GetCourseInfoAsync(long courseId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Course> PurchaseCourseAsync(AuthentificationToken token, long courseId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}