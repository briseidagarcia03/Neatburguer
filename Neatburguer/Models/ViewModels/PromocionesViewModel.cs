namespace Neatburguer.Models.ViewModels
{
    public class PromocionesViewModel
    {
        public IEnumerable<ModelPromociones> Promociones { get; set; } = null!;
        public int PrimerHambuMostrada { get; set; } = 0;
    }

    public class ModelPromociones
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal PrecioDePromocion { get; set; }
        public string Descripcion { get; set; } = null!;
    }
}
