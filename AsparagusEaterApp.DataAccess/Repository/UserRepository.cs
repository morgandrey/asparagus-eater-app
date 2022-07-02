using System.Linq.Expressions;
using AsparagusEaterApp.DataAccess.DataAccess;
using AsparagusEaterApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AsparagusEaterApp.DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly AsparagusEaterDbContext _dbContext;
    private readonly DbSet<User> dbSet;

    public UserRepository(AsparagusEaterDbContext dbContext)
    {
        _dbContext = dbContext;
        this.dbSet = dbContext.Set<User>();
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User> GetFirstOrDefaultAsync(Expression<Func<User, bool>> filter,
        string? includeProperties = null, bool tracked = true)
    {
        if (tracked)
        {
            IQueryable<User> query = dbSet;

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
        else
        {
            IQueryable<User> query = dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
    }

    public void UpdateUser(User user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _dbContext.Users.ToListAsync();
    }
}