using MedConnect_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MedConnect_API.UOW
{
    public class repo<T> where T :class
    {
        public HealthCareContext db;

        public repo(HealthCareContext db)
        {
            this.db = db;
        }

        public void Add(T t)
        {
            db.Set<T>().Add(t);
        }
        public List<T> Select()
        {
            return db.Set<T>().ToList();
        }

        public T SelectById<TT>(TT id)
        {
            return db.Set<T>().Find(id);
        }

        public void Delete<TT>(TT id)
        {
            db.Set<T>().Remove(SelectById(id));
        }
        public void Update(T entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }

}
