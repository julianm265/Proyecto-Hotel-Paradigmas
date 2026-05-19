namespace biblioteca_hotel.Modelos.Servicios
{
    public class ServicioLavanderia : Servicio_Hotelero
    {
        public ServicioLavanderia(string descripcion, decimal costo)
            : base(new Restaurante(), new Lavandera())
        {
            this.descripcion = descripcion;
            this.costo = costo;
        }
    }
}
