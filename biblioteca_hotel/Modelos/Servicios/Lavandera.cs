namespace biblioteca_hotel.Modelos.Servicios
{
    public class Lavandera : Interfaces.Iservicio_hotelero
    {
        public Lavandera()
        {
        }

        public void Brindar_servicio(Personas.Persona persona)
        {
        }

        public decimal GetPrecioPlanchada() => Utilidades.ReglasNegocioServicios.precio_planchada;
        public decimal GetPrecioPorPrenda() => Utilidades.ReglasNegocioServicios.Precio_por_prenda_lavand;
    }
}
