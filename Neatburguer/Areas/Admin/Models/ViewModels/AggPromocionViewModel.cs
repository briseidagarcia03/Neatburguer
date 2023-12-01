namespace Neatburguer.Areas.Admin.Models.ViewModels
{
    public class AggPromocionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal PrecioDePromocion { get; set; }
    }
}
