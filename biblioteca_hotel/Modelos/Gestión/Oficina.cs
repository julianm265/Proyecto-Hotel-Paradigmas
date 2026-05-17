using System.Collections.Generic;

namespace biblioteca_hotel.Modelos.Gestión
{
    public class Oficina : Interfaces.IOficina, Interfaces.IGestionHotel
    {
        protected List<Core.Reserva> l_reservas;
        protected List<Habitaciones.Habitacion> l_habitaciones_disp;
        protected List<Habitaciones.Habitacion> l_habitaciones_ocupadas;
        protected List<Habitaciones.Habitacion> l_habitaciones_reservadas;

        public Oficina()
        {
            l_reservas = new List<Core.Reserva>();
            l_habitaciones_disp = new List<Habitaciones.Habitacion>();
            l_habitaciones_ocupadas = new List<Habitaciones.Habitacion>();
            l_habitaciones_reservadas = new List<Habitaciones.Habitacion>();
        }

        public bool Revisar_habs_disp(string nro_hab)
        {
            foreach (var hab in l_habitaciones_disp)
            {
                if (hab.GetNumHab() == nro_hab)
                    return true;
            }
            return false;
        }

        public void cargar_datos(string archivo)
        {
        }

        public void procesar_factura(Habitaciones.Habitacion habitacion)
        {
        }

        public void gestionar_reserva(Core.Reserva reserva)
        {
            l_reservas.Add(reserva);
        }

        public void AgregarHabitacionDisp(Habitaciones.Habitacion hab) => l_habitaciones_disp.Add(hab);
        public void AgregarHabitacionOcupada(Habitaciones.Habitacion hab) => l_habitaciones_ocupadas.Add(hab);
        public void AgregarHabitacionReservada(Habitaciones.Habitacion hab) => l_habitaciones_reservadas.Add(hab);

        public Habitaciones.Habitacion[] GetHabitacionesDisp() => l_habitaciones_disp.ToArray();
        public Core.Reserva[] GetReservas() => l_reservas.ToArray();
    }
}
