using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class CartaService
    {
        protected List<Modelos.Carta.Oferta_carta> l_oferta_carta;

        public CartaService()
        {
            l_oferta_carta = new List<Modelos.Carta.Oferta_carta>();
        }

        public void Agregar(Modelos.Carta.Oferta_carta oferta_carta)
        {
            l_oferta_carta.Add(oferta_carta);
        }

        public Modelos.Carta.Oferta_carta[] ObtenerTodos()
        {
            return l_oferta_carta.ToArray();
        }

        public Modelos.Carta.Oferta_carta BuscarPorNombre(string nombre)
        {
            return l_oferta_carta.FirstOrDefault(o => o.GetType().Name == nombre);
        }

        public void CargarDatos(IEnumerable<Modelos.Carta.Oferta_carta> datos)
        {
            l_oferta_carta.Clear();
            if (datos != null)
                l_oferta_carta.AddRange(datos);
        }
    }
}
