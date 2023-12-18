using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

namespace Safit.Core.Services.Content;

public class ContentService : IContentService
{
    private IAuthentificationService authentificationService;
    private IRepositoryWrapper repositoryWrapper;

    public ContentService(
        IAuthentificationService authentificationService,
        IRepositoryWrapper repositoryWrapper)
    {
        this.authentificationService = authentificationService;
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<IEnumerable<CourseContent>> GetCourseContentAsync(long courseId, CancellationToken ct = default)
    {
        var selection = await repositoryWrapper.Course.FindByCondition(x => x.Id == courseId, ct);
        if (selection.Any() == false) throw new ArgumentException("course does not exist");
        var result = new List<CourseContent>();
        result.AddRange(selection.First().Videos.Select(x => new CourseContent() { Id = x.Id, ContentType = CourseContentType.Video }));
        result.AddRange(selection.First().Posts.Select(x => new CourseContent() { Id = x.Id, ContentType = CourseContentType.Post }));
        return result;
    }

    public async Task<Course> GetCourseInfoAsync(long courseId, CancellationToken ct = default)
    {
        var selection = await repositoryWrapper.Course.FindByCondition(x => x.Id == courseId, ct);
        if (selection.Any() == false) throw new ArgumentException("course does not exist");
        return selection.First();
    }

    public async Task<Course> PurchaseCourseAsync(AuthentificationToken token, long courseId, CancellationToken ct = default)
    {
        var userTokenInfo = await authentificationService.ExtractAsync(token, ct);
        var userCourseAccesses = await repositoryWrapper.CourseAccess.FindByCondition(x => x.UserId == userTokenInfo.Id, ct);
        if (!userCourseAccesses.Any()) throw new ArgumentException("user does not exist");
        var courses = await repositoryWrapper.Course.FindByCondition(x => x.Id == courseId, ct);
        if (!courses.Any()) throw new ArgumentException("course does not exist");
        var newCourseAccess = new CourseAccess() { UserId = userTokenInfo.Id, CourseId = courseId };
        await repositoryWrapper.CourseAccess.Create(newCourseAccess, ct);
        return newCourseAccess.Course;
    }
}
