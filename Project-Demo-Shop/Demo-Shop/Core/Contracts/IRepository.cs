using Demo_Shop.Core.Models;
using System.Linq;

namespace Demo_Shop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T t);
        void Update(T t);
    }
}