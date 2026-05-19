using System.Collections.Generic;

namespace biblioteca_hotel.Modelos.Core
{
    public class Factura : Interfaces.IGestionHotel
    {
        protected List<Productos.Producto> productos_consumidos;
        protected Personas.Persona persona;
        protected bool es_colombiano;
        protected List<Servicios.Servicio_Hotelero> l_servicios_consumidos;
        protected decimal costo_estancia;
        protected Habitaciones.Habitacion habitacion;
        protected int noches;

        public Factura(Personas.Persona persona, bool es_colombiano)
        {
            this.persona = persona;
            this.es_colombiano = es_colombiano;
            productos_consumidos = new List<Productos.Producto>();
            l_servicios_consumidos = new List<Servicios.Servicio_Hotelero>();
            costo_estancia = 0;
        }

        public void SetDetallesEstancia(Habitaciones.Habitacion habitacion, int noches, decimal costo_estancia)
        {
            this.habitacion = habitacion;
            this.noches = noches;
            this.costo_estancia = costo_estancia;
        }

        public void AgregarProducto(Productos.Producto producto)
        {
            productos_consumidos.Add(producto);
        }

        public void AgregarServicio(Servicios.Servicio_Hotelero servicio)
        {
            l_servicios_consumidos.Add(servicio);
        }

        public decimal CalcularSubtotal()
        {
            decimal subtotal = costo_estancia;
            foreach (var producto in productos_consumidos)
            {
                subtotal += producto.GetCosto();
            }
            foreach (var servicio in l_servicios_consumidos)
            {
                subtotal += servicio.GetCosto();
            }
            return subtotal;
        }

        public decimal CalcularImpuestos(bool es_colombiano)
        {
            decimal subtotal = CalcularSubtotal();
            if (es_colombiano)
            {
                return subtotal * (decimal)Utilidades.Regla_negocio.valor_IVA;
            }
            else
            {
                return subtotal * (decimal)Utilidades.Regla_negocio.seguro_hotelero;
            }
        }

        public decimal calcularTotal()
        {
            return CalcularSubtotal() + CalcularImpuestos(es_colombiano);
        }

        public void cargar_datos(string archivo)
        {
        }

        public void procesar_factura(Habitaciones.Habitacion habitacion)
        {
        }

        public void gestionar_reserva(Reserva reserva)
        {
        }

        public Personas.Persona GetPersona() => persona;
        public Productos.Producto[] GetProductos() => productos_consumidos.ToArray();
        public Servicios.Servicio_Hotelero[] GetServicios() => l_servicios_consumidos.ToArray();
    }
}
