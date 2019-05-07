using System.Collections.Generic;

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



}
