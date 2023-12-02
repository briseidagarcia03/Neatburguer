using Microsoft.AspNetCore.Mvc;
using Neatburguer.Models.ViewModels;
using Neatburguer.Repositories;

namespace Neatburguer.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuRepository repository;

        public MenuController(MenuRepository repository)
        {
            this.repository = repository;
        }

        [Route("Menu/{id?}")]
        public IActionResult Index(string? id)
        {
            id = id?.Replace("-", " ");

            //hambuguesa predimentada por si no hay escogido una 
            var hambu = new ModelHamburguesas
            {
                Id = 0,
                Nombre = "",
                Descripcion = "Escoge tu hamburguesa",
                Precio = 0,
                Clasificacion = "",
   
            };

            //todas las hamburguesas en lista y agrupadas
            var listahamburguesas = repository.GetAll().OrderBy(x => x.Nombre)
                .Select(x => new ModelHamburguesas
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripción,
                    Precio = x.Precio,
                    Clasificacion = x.IdClasificacionNavigation?.Nombre
                }).GroupBy(x => x.Clasificacion); ;

            //hamburguesa seleccionada
            if (!string.IsNullOrEmpty(id))
            {
                var hambuseleccionada = repository.GetByNombre(id);

                if (hambuseleccionada != null)
                {
                    hambu = new ModelHamburguesas
                    {
                        Id = hambuseleccionada.Id,
                        Nombre = hambuseleccionada.Nombre,
                        Descripcion = hambuseleccionada.Descripción,
                        Precio = hambuseleccionada.Precio,
                        Clasificacion = hambuseleccionada.IdClasificacionNavigation?.Nombre ?? ""
                    };
                }
            }

            //mostrar los valores
            var model = new MenuViewModel
            {
                Hamburguesa = hambu,
                Hamburguesas = listahamburguesas
            };

            return View(model);
        }


    }
}
