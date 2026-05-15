namespace biblioteca_hotel.Modelos.Servicios
{
    public class Restaurante : Interfaces.Iservicio_hotelero
    {
        public Restaurante()
        {
        }

        public void Brindar_servicio(Personas.Persona persona)
        {
        }

        public void Servicio_hab()
        {
        }

        public decimal GetCostoDesayuno() => Utilidades.ReglasNegocioServicios.Costo_desayuno;
        public decimal GetCostoAlmuerzo() => Utilidades.ReglasNegocioServicios.Costo_almuerzo;
        public decimal GetCostoCena() => Utilidades.ReglasNegocioServicios.costo_cena;
    }
}
