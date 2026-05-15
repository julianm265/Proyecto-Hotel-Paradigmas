using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class ReservaService
    {
        public Modelos.Core.Reserva[] l_reserva;
        private List<Modelos.Core.Reserva> _reservas;

        public ReservaService()
        {
            _reservas = new List<Modelos.Core.Reserva>();
            l_reserva = new Modelos.Core.Reserva[0];
        }

        public void Agregar(Modelos.Core.Reserva reserva)
        {
            _reservas.Add(reserva);
            l_reserva = _reservas.ToArray();
        }

        public Modelos.Core.Reserva[] ObtenerTodas()
        {
            return _reservas.ToArray();
        }

        public Modelos.Core.Reserva BuscarPorNumero(int numeroReserva)
        {
            return _reservas.ElementAtOrDefault(numeroReserva);
        }

        public Modelos.Core.Reserva[] BuscarPorcliente(string nombre)
        {
            return _reservas.Where(r => r.GetPersona().GetNombre() == nombre).ToArray();
        }

        public Dictionary<string, List<Modelos.Core.Reserva>> ResumenPorPersona()
        {
            var resumen = new Dictionary<string, List<Modelos.Core.Reserva>>();
            foreach (var reserva in _reservas)
            {
                string nombre = reserva.GetPersona().GetNombre();
                if (!resumen.ContainsKey(nombre))
                    resumen[nombre] = new List<Modelos.Core.Reserva>();
                resumen[nombre].Add(reserva);
            }
            return resumen;
        }
    }
}
