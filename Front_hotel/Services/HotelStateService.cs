using System;
using System.Collections.Generic;
using System.Linq;
using biblioteca_hotel.Eventos;
using biblioteca_hotel.Modelos.Core;
using biblioteca_hotel.Modelos.Gestión;
using biblioteca_hotel.Modelos.Habitaciones;
using biblioteca_hotel.Modelos.Personas;
using biblioteca_hotel.Modelos.Productos;
using biblioteca_hotel.Modelos.Servicios;
using biblioteca_hotel.Utilidades;

namespace Front_hotel.Services
{
    public class HotelStateService
    {
        private readonly List<Persona> _personas = new();
        private readonly List<Habitacion> _habitaciones = new();
        private readonly List<Reserva> _reservas = new();
        private readonly List<Factura> _facturas = new();
        private readonly Recepcion _recepcion = new();
        private readonly IAuditLogger _logger;

        public HotelStateService(IAuditLogger logger)
        {
            _logger = logger;
            Seed();
        }

        public Persona[] GetPersonas() => _personas.ToArray();
        public Habitacion[] GetHabitaciones() => _habitaciones.ToArray();
        public Reserva[] GetReservas() => _reservas.ToArray();
        public Factura[] GetFacturas() => _facturas.ToArray();

        public Persona CrearCliente(string nombre, string tipoId, string documento, string telefono, int codigo, float descuento)
        {
            var cliente = new Cliente(nombre, tipoId, documento, telefono, codigo, descuento);
            _personas.Add(cliente);
            _logger.Log($"Alta cliente: {nombre} ({documento})");
            return cliente;
        }

        public Persona CrearHuesped(string nombre, string tipoId, string documento, string telefono)
        {
            var huesped = new Huesped(nombre, tipoId, documento, telefono);
            _personas.Add(huesped);
            _logger.Log($"Alta huésped: {nombre} ({documento})");
            return huesped;
        }

        public Reserva CrearReserva(Persona persona, Habitacion habitacion, DateTime entrada, DateTime salida)
        {
            var reserva = new Reserva(persona, habitacion, entrada, salida);
            _reservas.Add(reserva);
            _logger.Log($"Reserva creada para {persona.GetNombre()} del {entrada:dd/MM} al {salida:dd/MM}");
            return reserva;
        }

        public void CheckIn(Persona persona, Habitacion habitacion)
        {
            _logger.Log($"Check-in registrado para {persona.GetNombre()}");
            _ = new EventoCheckInCompletado(persona.GetId(), habitacion);
        }

        public void CancelarReserva(Reserva reserva)
        {
            _reservas.Remove(reserva);
            _logger.Log($"Reserva cancelada de {reserva.GetPersona().GetNombre()}");
            _ = new EventoCancelacionReserva(reserva.GetPersona().GetId(), reserva.GetPersona().GetId());
        }

        public Factura CheckOut(Persona persona, bool esColombiano)
        {
            var factura = _recepcion.Check_Out(persona);
            _facturas.Add(factura);
            _logger.Log($"Check-out y factura creada para {persona.GetNombre()}");
            _ = new EventoFacturaGenerada(factura.GetHashCode().ToString(), factura.calcularTotal());
            return factura;
        }

        public void AgregarProductoFactura(Factura factura, Producto producto)
        {
            factura.AgregarProducto(producto);
            _logger.Log($"Producto agregado a factura de {factura.GetPersona().GetNombre()}");
        }

        public void AgregarServicioFactura(Factura factura, Servicio_Hotelero servicio)
        {
            factura.AgregarServicio(servicio);
            _logger.Log($"Servicio agregado a factura de {factura.GetPersona().GetNombre()}");
        }

        public void RegistrarConsumoMinibar(Habitacion habitacion, int cantidad)
        {
            _logger.Log($"Consumo minibar en {habitacion.GetType().Name}: {cantidad} unidad(es)");
            _ = new EventoConsumoMinibar(habitacion.GetHashCode(), cantidad);
        }

        private void Seed()
        {
            var cliente = new Cliente("Ana Torres", "DNI", "123456", "3124567890", 1201, 0.1f);
            var huesped = new Huesped("Luis Paredes", "PASAPORTE", "A92133", "3107778899");
            var huesped2 = new Huesped("Marina Díaz", "CE", "445566", "3205547788");

            _personas.AddRange(new Persona[] { cliente, huesped, huesped2 });

            var sencilla = new Sencilla(ReglasNegocioHabitaciones.costo_noche_hab_sencilla, "queen", 1);
            var ejecutiva = new Ejecutiva(ReglasNegocioHabitaciones.costo_noche_hab_ejecutiva, "king");
            var suite = new Suite(ReglasNegocioHabitaciones.costo_noche_suite, "king");

            _habitaciones.AddRange(new Habitacion[] { sencilla, ejecutiva, suite });

            _reservas.Add(new Reserva(cliente, sencilla, DateTime.Today, DateTime.Today.AddDays(3)));
            _reservas.Add(new Reserva(huesped, ejecutiva, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            var factura = new Factura(cliente, true);
            factura.AgregarProducto(new Agua(ReglasNegocioHabitaciones.precio_agua));
            factura.AgregarProducto(new Vino(ReglasNegocioHabitaciones.precio_vino));
            factura.AgregarProducto(new Kit_aseo(ReglasNegocioHabitaciones.precio_kit_aseo));
            factura.AgregarServicio(new Servicio_Hotelero(new Restaurante(), new Lavandera()));
            _facturas.Add(factura);

            var factura2 = new Factura(huesped2, false);
            factura2.AgregarProducto(new Gaseosa(ReglasNegocioHabitaciones.precio_gaseosa));
            factura2.AgregarProducto(new Bata_de_baño(ReglasNegocioHabitaciones.precio_bata));
            factura2.AgregarServicio(new Servicio_Hotelero(new Restaurante(), new Lavandera()));
            _facturas.Add(factura2);
        }
    }
}
