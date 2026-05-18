using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class HabitacionService
    {
        protected List<Modelos.Habitaciones.Habitacion> l_habitaciones;

        public HabitacionService()
        {
            l_habitaciones = new List<Modelos.Habitaciones.Habitacion>();
        }

        public void Agregar(Modelos.Habitaciones.Habitacion habitacion)
        {
            l_habitaciones.Add(habitacion);
        }

        public Modelos.Habitaciones.Habitacion[] ObtenerTodos()
        {
            return l_habitaciones.ToArray();
        }

        public Modelos.Habitaciones.Habitacion BuscarPornumero(string numero)
        {
            return l_habitaciones.FirstOrDefault(h => h.GetNumHab() == numero);
        }

        public decimal ObtenerCostoEstancia(string id_reserva)
        {
            return l_habitaciones.Count > 0 ? l_habitaciones[0].GetCostoNoche() : 0;
        }

        public void CargarDatos(IEnumerable<Modelos.Habitaciones.Habitacion> datos)
        {
            l_habitaciones.Clear();
            if (datos != null)
                l_habitaciones.AddRange(datos);
        }
    }
}
