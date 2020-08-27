using Demo_Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Demo_Shop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;

        List<T> items;

        string className;

        #region Constructor
        public InMemoryRepository()
        {
            className = typeof(T).Name;

            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }
        }
        #endregion

        public void Commit()
        {
            cache[className] = items;
        }

        #region Endpoints
        // Insert
        public void Insert(T t)
        {
            items.Add(t);
        }

        // Update
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }

            else
            {
                throw new Exception(className + " Not Found");
            }
        }

        //Find single T
        public T Find(string id)
        {
            T tToFind = items.Find(i => i.Id == id);

            if (tToFind !=null)
            {
                return tToFind;
            }

            else
            {
                throw new Exception(className + " Not Found");
            }
        }

        //Return a list of queryable T
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        //Delete a T
        public void Delete(string id)
        {
            T tToDelete = items.Find(i => i.Id == id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }

            else
            {
                throw new Exception(className + " Not Found");
            }
        }
        #endregion
    }
}