using System.Linq.Expressions;
using AsparagusEaterApp.DataAccess.Models;

namespace AsparagusEaterApp.DataAccess.Repository;

public interface IUserRepository
{
    Task Add(User user);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetFirstOrDefaultAsync(Expression<Func<User, bool>> filter, string? includeProperties = null, bool tracked = true);
    void UpdateUser(User user);
    Task SaveChangesAsync();
}