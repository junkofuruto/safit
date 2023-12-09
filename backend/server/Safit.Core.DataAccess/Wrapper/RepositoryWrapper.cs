using Safit.Core.DataAccess.Repository;
using Safit.Core.Domain.Model;
using System.Reflection.Emit;

namespace Safit.Core.DataAccess.Wrapper;

public class RepositoryWrapper
{
    private DatabaseContext context;
    private IUserRepository? userRepository;
    private ICartRepository? cartRepository;
    private ICartContentRepository? cartContentRepository;
    private ICourseRepository? courseRepository;
    private ICommentRepository? commentRepository;

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
    public ICourseRepository CourseRepository
    {
        get
        {
            if (courseRepository == null)
                courseRepository = new CourseRepository(context);
            return courseRepository;
        }
    }
    public ICommentRepository CommentRepository
    {
        get
        {
            if (commentRepository == null)
                commentRepository = new CommentRepository(context);
            return commentRepository;
        }
    }

    public RepositoryWrapper(DatabaseContext context)
    {
        this.context = context;
    }
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}