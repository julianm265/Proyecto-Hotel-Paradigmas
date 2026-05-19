using System;
using System.Collections.Generic;

namespace biblioteca_hotel.Modelos.Core
{
    /// <summary>
    /// Gestiona la facturación de huéspedes con cálculo de hospedaje, productos y servicios.
    /// </summary>
    public class Factura : Interfaces.IGestionHotel
    {
        protected List<Productos.Producto> productos_consumidos;
        protected Personas.Persona persona;
        protected bool es_colombiano;
        protected List<Servicios.Servicio_Hotelero> l_servicios_consumidos;
        protected decimal costo_estancia;
        protected Habitaciones.Habitacion habitacion;
        protected int noches;
        protected DateTime fecha_entrada;
        protected DateTime fecha_salida;

        /// <summary>
        /// Constructor original para facturación sin hospedaje.
        /// </summary>
        public Factura(Personas.Persona persona, bool es_colombiano)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona), "La persona no puede ser nula");

            this.persona = persona;
            this.es_colombiano = es_colombiano;
            productos_consumidos = new List<Productos.Producto>();
            l_servicios_consumidos = new List<Servicios.Servicio_Hotelero>();
            costo_estancia = 0;
            habitacion = null;
        }

        /// <summary>
        /// Constructor con habitación y fechas para facturación con hospedaje.
        /// </summary>
        public Factura(Personas.Persona persona, bool es_colombiano, 
                       Habitaciones.Habitacion habitacion, DateTime fecha_entrada, DateTime fecha_salida)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona), "La persona no puede ser nula");

            if (habitacion == null)
                throw new ArgumentNullException(nameof(habitacion), "La habitación no puede ser nula");

            if (fecha_salida <= fecha_entrada)
                throw new ArgumentException("La fecha de salida debe ser posterior a la de entrada", nameof(fecha_salida));

            if (string.IsNullOrWhiteSpace(habitacion.GetNumHab()))
                throw new ArgumentException("El número de habitación no puede ser nulo o vacío");

            this.persona = persona;
            this.es_colombiano = es_colombiano;
            this.habitacion = habitacion;
            this.fecha_entrada = fecha_entrada;
            this.fecha_salida = fecha_salida;
            this.noches = ObtenerNochesEstadia();
            this.costo_estancia = CalcularCostoHospedaje();
            productos_consumidos = new List<Productos.Producto>();
            l_servicios_consumidos = new List<Servicios.Servicio_Hotelero>();
        }

        public void SetDetallesEstancia(Habitaciones.Habitacion habitacion, int noches, decimal costo_estancia)
        {
            this.habitacion = habitacion;
            this.noches = noches;
            this.costo_estancia = costo_estancia;
        }

        /// <summary>
        /// Obtiene la cantidad de noches de estadía entre entrada y salida.
        /// </summary>
        public int ObtenerNochesEstadia()
        {
            if (habitacion == null)
                throw new InvalidOperationException("La habitación no está definida para calcular las noches");

            return (int)(fecha_salida - fecha_entrada).TotalDays;
        }

        /// <summary>
        /// Calcula el costo total de hospedaje multiplicando noches por costo por noche.
        /// </summary>
        public decimal CalcularCostoHospedaje()
        {
            if (habitacion == null)
                throw new InvalidOperationException("La habitación no está definida para calcular el costo de hospedaje");

            int nochesEstadia = ObtenerNochesEstadia();
            decimal costoNoche = habitacion.GetCostoNoche();
            return nochesEstadia * costoNoche;
        }

        /// <summary>
        /// Actualiza el descuento semanal del cliente si es aplicable.
        /// </summary>
        public void ActualizarDescuentoSemanal()
        {
            if (persona is Personas.Cliente cliente)
            {
                cliente.ActualizarDescuentoSemanal();
            }
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
