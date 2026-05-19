namespace biblioteca_hotel.Modelos.Servicios
{
    public class Servicio_Hotelero : Interfaces.Iservicio_hotelero
    {
        protected Restaurante restaurante;
        protected Lavandera lavanderia;
        protected decimal costo;
        protected string descripcion;

        public Servicio_Hotelero(Restaurante restaurante, Lavandera lavanderia)
        {
            this.restaurante = restaurante;
            this.lavanderia = lavanderia;
            costo = 0;
            descripcion = string.Empty;
        }

        public virtual void Brindar_servicio(Personas.Persona persona)
        {
        }

        public Restaurante GetRestaurante() => restaurante;
        public Lavandera GetLavanderia() => lavanderia;
        public virtual decimal GetCosto() => costo;
        public virtual string GetDescripcion() => descripcion;
    }
}
