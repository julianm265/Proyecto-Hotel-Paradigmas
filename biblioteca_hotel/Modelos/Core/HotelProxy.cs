using System.Reflection;

namespace biblioteca_hotel.Modelos.Core
{
    public class HotelProxy : Interfaces.IGestionHotel
    {
        protected Interfaces.IGestionHotel objetoReal;

        public HotelProxy(Interfaces.IGestionHotel objeto_real)
        {
            this.objetoReal = objeto_real;
        }

        public void cargar_datos(string archivo)
        {
            var metodo = objetoReal.GetType().GetMethod("cargar_datos");
            if (metodo != null)
            {
                metodo.Invoke(objetoReal, new object[] { archivo });
            }
        }

        public void procesar_factura(Habitaciones.Habitacion habitacion)
        {
            var metodo = objetoReal.GetType().GetMethod("procesar_factura");
            if (metodo != null)
            {
                metodo.Invoke(objetoReal, new object[] { habitacion });
            }
        }

        public void gestionar_reserva(Reserva reserva)
        {
            var metodo = objetoReal.GetType().GetMethod("gestionar_reserva");
            if (metodo != null)
            {
                metodo.Invoke(objetoReal, new object[] { reserva });
            }
        }
    }
}
