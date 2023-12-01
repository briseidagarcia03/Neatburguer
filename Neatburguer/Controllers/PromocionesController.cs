using Microsoft.AspNetCore.Mvc;
using Neatburguer.Models.Entities;
using Neatburguer.Models.ViewModels;
using Neatburguer.Repositories;

namespace Neatburguer.Controllers
{
    public class PromocionesController : Controller
    {
        private readonly Repository<Menu> repository;
        public PromocionesController(Repository<Menu> repository)
        {
            this.repository = repository;
        }

        [Route("Promociones/{id?}")]
        public IActionResult Index(string id)
        {
            var promos = repository.GetAll().Where(x => x.PrecioPromocion != null).ToList();

            if (promos.Any())
            {
                var idHambuPromo = promos.First().Id;

                var vm = new PromocionesViewModel
                {
                    PrimerHambuMostrada = idHambuPromo,
                    Promociones = promos.Select(x => new ModelPromociones
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = (decimal)x.Precio,
                        PrecioDePromocion = (decimal)x.PrecioPromocion,
                        Descripcion = x.Descripción
                    }),
                };

                return View(vm);
            }

            return View(new PromocionesViewModel());
        }


        //[Route("promociones/{id?}")]
        //public IActionResult Index(string Id)
        //{
        //    var promos = repository.GetAll().Where(x => x.PrecioPromocion != null).ToList(); // Asegurarse de que sea una lista

        //    if (promos.Any())
        //    {
        //        var primerIdHambuPromo = promos.First().Id;
        //        var vm = new PromocionesViewModel()
        //        {
        //            PrimerHambuMostrada = primerIdHambuPromo,
        //            Promociones = promos.Select(x => new ModelPromociones()
        //            {
        //                Id = x.Id,
        //                Nombre = x.Nombre,
        //                Precio = (decimal)x.Precio,
        //                PrecioDePromocion = (decimal)x.PrecioPromocion,
        //                Descripcion = x.Descripción
        //            }),
        //        };
        //        return View(vm);
        //    }

        //    var vmSinPromos = new PromocionesViewModel(); // Crear una instancia vacía si no hay promociones
        //    return View(vmSinPromos);
        //}

    }
}
