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
    }
}