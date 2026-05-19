using System;

namespace biblioteca_hotel.Modelos.Core
{
    public class Reserva : Interfaces.IGestionHotel
    {
        protected Personas.Persona persona;
        protected Habitaciones.Habitacion habitacion;
        protected DateTime fecha_entrada;
        protected DateTime fecha_salida;
        protected bool check_in_realizado;

        public Reserva(Personas.Persona persona, Habitaciones.Habitacion habitacion, DateTime fecha_entrada, DateTime fecha_salida)
        {
            this.persona = persona;
            this.habitacion = habitacion;
            this.fecha_entrada = fecha_entrada;
            this.fecha_salida = fecha_salida;
            check_in_realizado = false;
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
        public Habitaciones.Habitacion GetHabitacion() => habitacion;
        public DateTime GetFechaEntrada() => fecha_entrada;
        public DateTime GetFechaSalida() => fecha_salida;
        public bool GetCheckInRealizado() => check_in_realizado;
        public void SetCheckInRealizado(bool realizado) => check_in_realizado = realizado;
    }
}
