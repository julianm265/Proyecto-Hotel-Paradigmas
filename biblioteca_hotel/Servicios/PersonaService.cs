using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    /// <summary>
    /// Servicio para gestionar personas (clientes, huéspedes) con generación de códigos y descuentos.
    /// </summary>
    public class PersonaService
    {
        private static readonly Random _random = new();
        private static readonly object _lockObject = new object();
        protected List<Modelos.Personas.Persona> l_personas;

        public PersonaService()
        {
            l_personas = new List<Modelos.Personas.Persona>();
        }

        public void Agregar(Modelos.Personas.Persona persona)
        {
            l_personas.Add(persona);
        }

        public Modelos.Personas.Persona[] ObtenerTodos()
        {
            return l_personas.ToArray();
        }

        public Modelos.Personas.Persona BuscarPorId(string id)
        {
            return l_personas.FirstOrDefault(p => p.GetId() == id);
        }

        public Modelos.Personas.Persona[] BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return l_personas.ToArray();
            return l_personas
                .Where(p => p.GetNombre().Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        public Modelos.Personas.Persona[] BuscarPorFiltro(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro)) return l_personas.ToArray();
            return l_personas
                .Where(p =>
                    p.GetNombre().Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    p.GetId().Contains(filtro, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        public void CargarDatos(IEnumerable<Modelos.Personas.Persona> datos)
        {
            l_personas.Clear();
            if (datos != null)
                l_personas.AddRange(datos);
        }

        /// <summary>
        /// Genera un código de cliente único entre 1000 y 999998.
        /// Thread-safe mediante lock.
        /// </summary>
        public static int GenerarCodigoCliente()
        {
            lock (_lockObject)
            {
                return _random.Next(1000, 999999);
            }
        }

        /// <summary>
        /// Obtiene el número de semana del año actual según ISO 8601.
        /// </summary>
        private static int ObtenerSemanaDelAno(DateTime? fecha = null)
        {
            DateTime hoy = fecha ?? DateTime.Now;
            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar cal = ci.Calendar;

            CalendarWeekRule regla = CalendarWeekRule.FirstFourDayWeek;
            DayOfWeek primerDia = ci.DateTimeFormat.FirstDayOfWeek;

            return cal.GetWeekOfYear(hoy, regla, primerDia);
        }

        /// <summary>
        /// Genera un descuento semanal consistente entre 0 y 0.20 (0% a 20%).
        /// El descuento es el mismo durante toda la semana y varía cada semana.
        /// Utiliza la semana actual como semilla para reproducibilidad.
        /// </summary>
        public static float GenerarDescuentoSemanal(DateTime? fecha = null)
        {
            int semanaActual = ObtenerSemanaDelAno(fecha);

            // Usar la semana como semilla para generar un valor consistente
            Random randomSemanal = new Random(semanaActual);

            // Descuento entre 0 y 0.20 (0% a 20%)
            return (float)(randomSemanal.NextDouble() * 0.20);
        }
    }
}
