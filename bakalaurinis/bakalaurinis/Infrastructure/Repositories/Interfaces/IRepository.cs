using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<ICollection<TEntity>> GetAll();
        public Task<TEntity> GetById(int id);
        public Task<int> Create(TEntity entity);
        public Task<bool> Update(TEntity entity);
        public Task<bool> Delete(TEntity entity);
    }
}
