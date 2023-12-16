using Microsoft.EntityFrameworkCore;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Repository;
using Safit.Core.Services.ProfileManagement.Exceptions;

namespace Safit.Core.Services.ProfileManagement;

public class ProfileService : IProfileManagementService
{
    private IRepositoryWrapper repositoryWrapper;

    public ProfileService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<User> GetUserAsync(string username, CancellationToken ct = default)
    {
        var user = await repositoryWrapper.User.FindByCondition(x => x.Username == username, ct);
        var selected = await user.FirstOrDefaultAsync(ct);
        if (selected == null) throw new UserNotFoundException();
        else return selected;
    }
    public async Task<User> GetUserAsync(long id, CancellationToken ct = default)
    {
        var user = await repositoryWrapper.User.FindByCondition(x => x.Id == id, ct);
        var selected = await user.FirstOrDefaultAsync(ct);
        if (selected == null) throw new UserNotFoundException();
        else return selected;
    }
    public async Task UpdateInformationAsync(User user, CancellationToken ct = default)
    {
        await repositoryWrapper.User.Update(user, ct);
        await repositoryWrapper.SaveChangesAsync();
    }
    public async Task UpdateBalanceAsync(User user, decimal balance, CancellationToken ct = default)
    {
        var result = await repositoryWrapper.User.FindByCondition(x => x.Id == user.Id);
        var selected = await result.FirstAsync();
        selected.Balance = balance;
        await repositoryWrapper.User.Update(user, ct);
        await repositoryWrapper.SaveChangesAsync();
    }
    public async Task<List<Course>> GetCoursesAsync(User user, CancellationToken ct = default)
    {
        var courses = await repositoryWrapper.CourseAccess.FindByCondition(x => x.UserId == user.Id, ct);
        return await courses.Select(x => x.Course).ToListAsync(ct); 
    }
    public async Task<CourseAccess> BuyCourseAsync(User user, Course course, CancellationToken ct = default)
    {
        var entity = new CourseAccess() { UserId = user.Id, CourseId = course.Id };
        await repositoryWrapper.CourseAccess.Create(entity, ct);
        await repositoryWrapper.SaveChangesAsync();
        return entity;
    }
    public async Task<Trainer> PromoteUserAsync(User user, CancellationToken ct = default)
    {
        var entity = new Trainer() { Id = user.Id };
        await repositoryWrapper.Trainer.Create(entity, ct);
        await repositoryWrapper.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> IsUserPromotedAsync(User user, CancellationToken ct = default)
    {
        var selection = await repositoryWrapper.Trainer.FindByCondition(x => x.Id == user.Id, ct);
        var trainer = await selection.FirstOrDefaultAsync(ct);
        return trainer != null;
    }
    public async Task<IEnumerable<Trainer>> GetSubscribtionAsync(User user, CancellationToken ct = default)
    {
        return await Task.Run(() => user.Trainers.Select(x => x), ct);
    }
}