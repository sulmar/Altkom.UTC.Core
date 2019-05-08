using System.Collections.Generic;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.IServices
{
    public interface IEntiesService<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(int id);        
    }


    public interface IEntiesServiceAsync<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
    }



}
