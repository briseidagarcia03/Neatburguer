using Microsoft.EntityFrameworkCore;
using Neatburguer.Models.Entities;
using Neatburguer.Models.ViewModels;
using NuGet.Protocol.Core.Types;
using System;

namespace Neatburguer.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {

        }

        public override IEnumerable<Menu> GetAll()
        {
            return Context.Menu.Include(x => x.IdClasificacionNavigation).OrderBy(x => x.Nombre);
        }
        public Menu? GetByNombre(string n)
        {
            return Context.Menu.Include(x => x.IdClasificacionNavigation).FirstOrDefault(x => x.Nombre == n);
        }
    }
}

