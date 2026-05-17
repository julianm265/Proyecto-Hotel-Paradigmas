namespace biblioteca_hotel.Interfaces
{
    public interface IGestionHotel
    {
        void cargar_datos(string archivo);
        void procesar_factura(Modelos.Habitaciones.Habitacion habitacion);
        void gestionar_reserva(Modelos.Core.Reserva reserva);
    }
}
