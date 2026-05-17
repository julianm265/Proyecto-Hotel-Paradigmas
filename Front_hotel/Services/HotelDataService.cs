using System;
using System.Collections.Generic;
using biblioteca_hotel.Modelos.Core;
using biblioteca_hotel.Modelos.Habitaciones;
using biblioteca_hotel.Modelos.Personas;
using biblioteca_hotel.Modelos.Productos;
using biblioteca_hotel.Modelos.Servicios;
using biblioteca_hotel.Utilidades;

namespace Front_hotel.Services
{
    public class HotelDataService
    {
        private readonly List<Persona> _personas = new();
        private readonly List<Habitacion> _habitaciones = new();
        private readonly List<Reserva> _reservas = new();
        private readonly List<Factura> _facturas = new();

        public HotelDataService()
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

        public Persona[] GetPersonas() => _personas.ToArray();
        public Habitacion[] GetHabitaciones() => _habitaciones.ToArray();
        public Reserva[] GetReservas() => _reservas.ToArray();
        public Factura[] GetFacturas() => _facturas.ToArray();
    }
}
