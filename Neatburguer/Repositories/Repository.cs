using Neatburguer.Models.Entities;

namespace Neatburguer.Repositories
{
    public class Repository<T> where T : class
    {
        public Repository(NeatContext context) 
        {
            Context = context;
        }
        public NeatContext Context { get; }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual T? Get(object Id)
        {
            return Context.Find<T>(Id);
        }

        public virtual void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        { 
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(object Id)
        {
            var entity = Get(Id);
            if(entity != null)
            {
                Delete(entity);   
            }
        }
    }
}