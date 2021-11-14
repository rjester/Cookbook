using Ardalis.Specification.EntityFrameworkCore;
using Cookbook.SharedKernel.Interfaces;

namespace Cookbook.Infrastructure.Data
{
    public class RecipeRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    //public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public RecipeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
