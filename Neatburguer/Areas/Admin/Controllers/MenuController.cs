using Microsoft.AspNetCore.Mvc;
using Neatburguer.Areas.Admin.Models.ViewModels;
using Neatburguer.Models.Entities;
using Neatburguer.Repositories;

namespace Neatburguer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly MenuRepository menuRepository;
        private readonly Repository<Clasificacion> clasirepository;

        public MenuController(MenuRepository menuRepository, Repository<Clasificacion> clasi)
        {
            this.menuRepository = menuRepository;
            this.clasirepository = clasi;
        }

        public IActionResult Index()
        {
            var vm = menuRepository.GetAll().Select(x => new MenuAdminViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripción,
                Precio = (decimal)x.Precio,
                PrecioPromocion = (decimal)(x.PrecioPromocion ?? 0),
                Clasificacion = x.IdClasificacionNavigation.Nombre
            }).GroupBy(x => x.Clasificacion);
            return View(vm);
        }

        //cargar las clasificaciones
        public IActionResult AgregarHambu()
        {
            var clasi = clasirepository.GetAll().Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
            });
            //llenar la lista con las clasificaciones que hay
            var vm = new AggHambuViewModel
            {
                ListaClasificaciones = clasi
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AgregarHambu(AggHambuViewModel vm)
        {
            if (vm.Img != null)
            {
                if (vm.Img.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes PNG.");
                }
                if (vm.Img.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500kb");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Nombre))
                ModelState.AddModelError("", "Necesita escribir el nombre de la hamburguesa");
            if (vm.Precio == 0)
                ModelState.AddModelError("", "Necesita el precio de la hamburguesa");
            if (vm.Precio < 0)
                ModelState.AddModelError("", "El precio debe ser un número valido.");
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
                ModelState.AddModelError("", "Necesita escribir la descripcion de la hamburguesa");
            if (vm.IdClasificacion == 0)
                ModelState.AddModelError("", "Necesita escribir la clasificación");
            vm.ListaClasificaciones = clasirepository.GetAll().Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
            });
            if (ModelState.IsValid)
            {
                var hambu = new Menu
                {
                    Nombre = vm.Nombre,
                    Descripción = vm.Descripcion,
                    Precio = (double)vm.Precio,
                    IdClasificacion = vm.IdClasificacion,
                };
                menuRepository.Insert(hambu);
                if (vm.Img == null)
                {
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{hambu.Id}.png");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{hambu.Id}.png");
                    vm.Img.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }
            return View(vm);

        }

        public IActionResult Editar(int id)
        {
            var hambu = menuRepository.Get(id);
            var clasi = clasirepository.GetAll().Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
            });
            if (hambu == null)
            {
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }
            else
            {
                var vm = new AggHambuViewModel
                {
                    Id = hambu.Id,
                    Nombre = hambu.Nombre,
                    Descripcion = hambu.Descripción,
                    Precio = (decimal)hambu.Precio,
                    IdClasificacion = hambu.IdClasificacion,
                    ListaClasificaciones = clasi
                };
                return View(vm);
            }

        }

        [HttpPost]
        public IActionResult Editar(AggHambuViewModel vm)
        {
            if (vm.Img != null)
            {
                if (vm.Img.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes PNG.");
                }
                if (vm.Img.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500kb");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Nombre))
                ModelState.AddModelError("", "Necesita escribir el nombre de la hamburguesa");
            if (vm.Precio == 0)
                ModelState.AddModelError("", "Necesita el precio de la hamburguesa");
            if (vm.Precio < 0)
                ModelState.AddModelError("", "El precio debe ser un número valido.");
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
                ModelState.AddModelError("", "Necesita escribir la descripcion de la hamburguesa");
            if (vm.IdClasificacion == 0)
                ModelState.AddModelError("", "Necesita escribir la clasificación");
            var clasi = clasirepository.GetAll().Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
            });
            if (ModelState.IsValid)
            {
                var hambu = menuRepository.Get(vm.Id);
                if (hambu == null)
                {
                    return RedirectToAction("Index", "Menu", new { area = "Admin" });
                }

                hambu.Nombre = vm.Nombre;
                hambu.Descripción = vm.Descripcion;
                hambu.Precio = (double)vm.Precio;
                hambu.IdClasificacion = vm.IdClasificacion;

                menuRepository.Update(hambu);
                if (vm.Img != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{hambu.Id}.png");
                    vm.Img.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }

            vm.ListaClasificaciones = clasi;
            return View(vm);
        }

        public IActionResult Eliminar(int id)
        {
            var hambu = menuRepository.Get(id);
            if (hambu != null)
            {
                var vm = new EliminarHambuViewModel
                {
                    Id = hambu.Id,
                    Nombre = hambu.Nombre,
                };
                return View(vm);
            }
            return RedirectToAction("Index", "Menu", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult Eliminar(EliminarHambuViewModel vm)
        {
            var hambu = menuRepository.Get(vm.Id);
            if (hambu == null)
            {
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }
            menuRepository.Delete(vm.Id);
            var ruta = $"wwwroot/hamburguesas/{vm.Id}.png";
            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }
            return RedirectToAction("Index", "Menu", new { area = "Admin" });

        }

        public IActionResult AgregarPromo(int id)
        {
            var p = menuRepository.Get(id);
            if (p == null)
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            else
            {
                var vm = new AggPromocionViewModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = (decimal)p.Precio,
                    PrecioDePromocion = (decimal?)p.PrecioPromocion ?? 0
                };
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult AgregarPromo(AggPromocionViewModel vm)
        {
            if (vm.PrecioDePromocion == 0)
            {
                ModelState.AddModelError("", "Necesita escribir el precio de la promoción");
            }
            if (ModelState.IsValid)
            {
                var hambu = menuRepository.Get(vm.Id);
                if (hambu == null)
                    return RedirectToAction("Index", "Menu", new { area = "Admin" });
                hambu.PrecioPromocion = (double?)vm.PrecioDePromocion;
                menuRepository.Update(hambu);
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }
            return View(vm);
        }

        public IActionResult EliminarPromo(int Id)
        {
            var p = menuRepository.Get(Id);
            if (p == null)
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            else
            {
                AggPromocionViewModel vm = new()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = (decimal)p.Precio,
                    PrecioDePromocion = (decimal)p.PrecioPromocion
                };
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult EliminarPromo(AggPromocionViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var hambu = menuRepository.Get(vm.Id);
                if(hambu == null)
                    return RedirectToAction("Index", "Menu", new { area = "Admin" });
                hambu.PrecioPromocion = null; //quitar la promo y mandar cambios
                menuRepository.Update(hambu);
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }
            return View(vm);
        }
    }
}
