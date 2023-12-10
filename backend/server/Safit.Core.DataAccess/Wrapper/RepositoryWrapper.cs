using Safit.Core.DataAccess.Repository;

namespace Safit.Core.DataAccess.Wrapper;

public class RepositoryWrapper : IRepositoryWrapper
{
    private DatabaseContext context;
    private IUserRepository? userRepository;
    private ICartRepository? cartRepository;
    private ICartContentRepository? cartContentRepository;
    private ICourseRepository? courseRepository;
    private ICommentRepository? commentRepository;
    private ICourseAccessRepository? courseAccessRepository;
    private IFetchSourceRepository? fetchSourceRepository;
    private IPostRepository? postRepositoryWrapper;
    private IProductRepository? productRepository;
    private IRecommendationRepository? recommendationRepository;
    private ISpecialisationRepository? specialisationRepository;
    private ISportRepository? sportRepository;
    private ITagRepository? tagRepository;
    private ITrainerRepository? trainerRepository;
    private IVideoRepository? videoRepository;

    public RepositoryWrapper(DatabaseContext context)
    {
        this.context = context;
    }

    public IUserRepository User
    {
        get
        {
            if (userRepository == null)
                userRepository = new UserRepository(context);
            return userRepository;
        }
    }
    public ICartRepository Cart
    {
        get
        {
            if (cartRepository == null)
                cartRepository = new CartRepository(context);
            return cartRepository;
        }
    }
    public ICartContentRepository CartContent
    {
        get
        {
            if (cartContentRepository == null)
                cartContentRepository = new CartContentRepository(context);
            return cartContentRepository;
        }
    }
    public ICourseRepository Course
    {
        get
        {
            if (courseRepository == null)
                courseRepository = new CourseRepository(context);
            return courseRepository;
        }
    }
    public ICommentRepository Comment
    {
        get
        {
            if (commentRepository == null)
                commentRepository = new CommentRepository(context);
            return commentRepository;
        }
    }
    public ICourseAccessRepository CourseAccess
    {
        get
        {
            if (courseAccessRepository == null)
                courseAccessRepository = new CourseAccessRepository(context);
            return courseAccessRepository;
        }
    }
    public IFetchSourceRepository FetchSource
    {
        get
        {
            if (fetchSourceRepository == null)
                fetchSourceRepository = new FetchSourceRepository(context);
            return fetchSourceRepository;
        }
    }
    public IPostRepository Post
    {
        get
        {
            if (postRepositoryWrapper == null)
                postRepositoryWrapper = new PostRepository(context);
            return postRepositoryWrapper;
        }
    }
    public IProductRepository Product
    {
        get
        {
            if (productRepository == null)
                productRepository = new ProductRepository(context);
            return productRepository;
        }
    }
    public IRecommendationRepository Recommendation
    {
        get
        {
            if (recommendationRepository == null)
                recommendationRepository = new RecommendationRepository(context);
            return recommendationRepository;
        }
    }
    public ISpecialisationRepository Specialisation
    {
        get
        {
            if (specialisationRepository == null)
                specialisationRepository = new SpecialisationRepository(context);
            return specialisationRepository;
        }
    }
    public ISportRepository Sport
    {
        get
        {
            if (sportRepository == null)
                sportRepository = new SportRepository(context);
            return sportRepository;
        }
    }
    public ITagRepository Tag
    {
        get
        {
            if (tagRepository == null)
                tagRepository = new TagRepository(context);
            return tagRepository;
        }
    }
    public ITrainerRepository Trainer
    {
        get
        {
            if (trainerRepository == null)
                trainerRepository = new TrainerRepository(context);
            return trainerRepository;
        }
    }
    public IVideoRepository Video
    {
        get
        {
            if (videoRepository == null)
                videoRepository = new VideoRepository(context);
            return videoRepository;
        }
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}