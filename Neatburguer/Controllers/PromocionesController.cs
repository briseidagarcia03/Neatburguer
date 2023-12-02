using Microsoft.AspNetCore.Mvc;
using Neatburguer.Models.Entities;
using Neatburguer.Models.ViewModels;
using Neatburguer.Repositories;
using System;
using System.Linq;

namespace Neatburguer.Controllers
{
    public class PromocionesController : Controller
    {
        private readonly Repository<Menu> repository;

        public PromocionesController(Repository<Menu> repository)
        {
            this.repository = repository;
        }

        public IActionResult Index(int id = 0)
        {
            var promos = repository.GetAll().Where(x => x.PrecioPromocion != null).ToList();

            if (promos.Any())
            {
                //para que no se exceda el numero de hambus en la lista
                var idHambuPromo = Math.Min(id, promos.Count - 1);

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

            return RedirectToAction("Index", "Home");
        }
    }
}



