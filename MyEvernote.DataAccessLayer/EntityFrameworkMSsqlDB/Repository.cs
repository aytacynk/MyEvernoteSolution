using MyEvernote.Common;
using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        //private DatabaseContex db;
        private DbSet<T> _objectSet;

        public Repository()
        {
            // Singleton Pattern'den gelen static metotlar ile alıyoruz db'yi. db'nin adı contex yaptık.
            _objectSet = contex.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUsername = App.Common.GetCuurentUsername(); //TODO işlem yapan kullanıcı adı yazılacak 
            }

            return Save();
        }

        public int Update(T obj)
        {

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCuurentUsername();  //TODO işlem yapan kullanıcı adı yazılacak 
            }

            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return contex.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}
