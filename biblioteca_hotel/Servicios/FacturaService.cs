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

        /// <summary>
        /// Agrega un producto a la factura activa de la persona.
        /// Si no existe factura, la crea automáticamente (es_colombiano = true por defecto).
        /// </summary>
        public void ActualizarConsumo(Modelos.Personas.Persona persona, Modelos.Productos.Producto producto)
        {
            var factura = _facturas.FirstOrDefault(f => f.GetPersona().GetId() == persona.GetId());
            if (factura == null)
            {
                factura = new Modelos.Core.Factura(persona, true);
                _facturas.Add(factura);
                l_facturas = _facturas.ToArray();
            }
            factura.AgregarProducto(producto);
        }

        public void ActualizarServicio(Modelos.Personas.Persona persona, Modelos.Servicios.Servicio_Hotelero servicio)
        {
            var factura = _facturas.FirstOrDefault(f => f.GetPersona().GetId() == persona.GetId());
            if (factura == null)
            {
                factura = new Modelos.Core.Factura(persona, true);
                _facturas.Add(factura);
                l_facturas = _facturas.ToArray();
            }
            factura.AgregarServicio(servicio);
        }

        public Modelos.Core.Factura ObtenerOCrearFactura(Modelos.Personas.Persona persona, bool esColombiano = true)
        {
            var factura = _facturas.FirstOrDefault(f => f.GetPersona().GetId() == persona.GetId());
            if (factura == null)
            {
                factura = new Modelos.Core.Factura(persona, esColombiano);
                _facturas.Add(factura);
                l_facturas = _facturas.ToArray();
            }
            return factura;
        }

        public void CargarDatos(IEnumerable<Modelos.Core.Factura> datos)
        {
            _facturas.Clear();
            if (datos != null)
            {
                _facturas.AddRange(datos);
            }
            l_facturas = _facturas.ToArray();
        }
    }
}
