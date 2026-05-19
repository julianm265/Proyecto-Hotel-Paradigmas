using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class PersonaService
    {
        private static readonly System.Random _random = new();
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
                .Where(p => p.GetNombre().Contains(nombre, System.StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        public Modelos.Personas.Persona[] BuscarPorFiltro(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro)) return l_personas.ToArray();
            return l_personas
                .Where(p =>
                    p.GetNombre().Contains(filtro, System.StringComparison.OrdinalIgnoreCase) ||
                    p.GetId().Contains(filtro, System.StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        public void CargarDatos(IEnumerable<Modelos.Personas.Persona> datos)
        {
            l_personas.Clear();
            if (datos != null)
                l_personas.AddRange(datos);
        }

        public static int GenerarCodigoCliente()
        {
            return _random.Next(1000, 9999);
        }

        public static float GenerarDescuentoSemanal(System.DateTime? fecha = null)
        {
            var fechaEvaluacion = fecha ?? System.DateTime.Now;
            return fechaEvaluacion.DayOfWeek == System.DayOfWeek.Wednesday ? 0.15f : 0f;
        }
    }
}
