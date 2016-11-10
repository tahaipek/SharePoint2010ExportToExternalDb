using System.Collections.Generic;

namespace SpAktarim.Bll.Repositories
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> All();
        T FindById(long id);
        void Remove(long id);
        void Remove(T entity);
        void Update(T entity);
    }
}