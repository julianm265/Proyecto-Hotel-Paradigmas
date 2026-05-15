using System.Collections.Generic;

namespace biblioteca_hotel.Modelos.Core
{
    public class Piso
    {
        protected List<Habitaciones.Habitacion> l_habitaciones;
        protected int numero;

        public Piso(int numero)
        {
            this.numero = numero;
            l_habitaciones = new List<Habitaciones.Habitacion>();
        }

        public void AgregarHabitacion(Habitaciones.Habitacion habitacion)
        {
            l_habitaciones.Add(habitacion);
        }

        public Habitaciones.Habitacion[] GetHabitaciones() => l_habitaciones.ToArray();
        public int GetNumero() => numero;
    }
}
