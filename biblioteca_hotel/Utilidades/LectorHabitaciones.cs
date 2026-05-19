using System.Collections.Generic;
using System.IO;

namespace biblioteca_hotel.Utilidades
{
    public class LectorHabitaciones : Interfaces.IGestionHotel
    {
        protected string ruta_archivo;
        protected List<Modelos.Habitaciones.Habitacion> lista_habitaciones;

        public LectorHabitaciones(string ruta_archivo)
        {
            this.ruta_archivo = ruta_archivo;
            lista_habitaciones = new List<Modelos.Habitaciones.Habitacion>();
        }

        public void cargar_datos(string archivo)
        {
            if (!File.Exists(archivo))
                return;

            var lineas = File.ReadAllLines(archivo);
            foreach (var linea in lineas)
            {
                procesar_linea(linea);
            }
        }

        protected void procesar_linea(string linea)
        {
            if (string.IsNullOrWhiteSpace(linea)) return;

            var partes = linea.Split(',');
            if (partes.Length < 4) return;

            var tipo = partes[0].Trim().ToLower();
            var num = partes[1].Trim();
            var piso = int.TryParse(partes[2].Trim(), out var pisoVal) ? pisoVal : 1;
            var cama = partes.Length > 4 ? partes[4].Trim() : "doble";

            Modelos.Habitaciones.Habitacion habitacion = tipo switch
            {
                "ejecutiva" => new Modelos.Habitaciones.Ejecutiva(num, cama, piso),
                "suite" => new Modelos.Habitaciones.Suite(num, cama, piso),
                _ => new Modelos.Habitaciones.Sencilla(num, cama, piso)
            };

            lista_habitaciones.Add(habitacion);
        }

        public void procesar_factura(Modelos.Habitaciones.Habitacion habitacion)
        {
        }

        public void gestionar_reserva(Modelos.Core.Reserva reserva)
        {
        }

        public Modelos.Habitaciones.Habitacion[] GetHabitaciones() => lista_habitaciones.ToArray();
    }
}
