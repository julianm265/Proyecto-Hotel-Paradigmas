using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class PersonaService
    {
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
    }
}
