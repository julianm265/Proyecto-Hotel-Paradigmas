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
        }

        public Persona[] GetPersonas() => _personas.ToArray();
        public Habitacion[] GetHabitaciones() => _habitaciones.ToArray();
        public Reserva[] GetReservas() => _reservas.ToArray();
        public Factura[] GetFacturas() => _facturas.ToArray();
    }
}
