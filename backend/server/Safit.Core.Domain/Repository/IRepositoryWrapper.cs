namespace Safit.Core.Domain.Repository;

public interface IRepositoryWrapper
{
    public ICartRepository Cart { get; }
    public ICartContentRepository CartContent { get; }
    public ICommentRepository Comment { get; }
    public ICourseRepository Course { get; }
    public ICourseAccessRepository CourseAccess { get; }
    public IFetchSourceRepository FetchSource { get; }
    public IPostRepository Post { get; }
    public IProductRepository Product { get; }
    public IRecommendationRepository Recommendation { get; }
    public ISpecialisationRepository Specialisation { get; }
    public ISportRepository Sport { get; }
    public ITagRepository Tag { get; }
    public ITrainerRepository Trainer { get; }
    public IUserRepository User { get; }
    public IVideoRepository Video { get; }

    public Task SaveChangesAsync();
}