using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class FacturaService
    {
        public Modelos.Core.Factura[] l_facturas;
        private List<Modelos.Core.Factura> _facturas;

        public FacturaService()
        {
            _facturas = new List<Modelos.Core.Factura>();
            l_facturas = new Modelos.Core.Factura[0];
        }

        public void Agregar(Modelos.Core.Factura factura)
        {
            _facturas.Add(factura);
            l_facturas = _facturas.ToArray();
        }

        public Modelos.Core.Factura[] ObtenerTodas()
        {
            return _facturas.ToArray();
        }

        public Modelos.Core.Factura BuscarPorCodigo(string codigoFactura)
        {
            return _facturas.FirstOrDefault(f => f.GetHashCode().ToString() == codigoFactura);
        }

        public Modelos.Core.Factura[] BuscarPorcliente(string nombre)
        {
            return _facturas.Where(f => f.GetPersona().GetNombre() == nombre).ToArray();
        }
    }
}
