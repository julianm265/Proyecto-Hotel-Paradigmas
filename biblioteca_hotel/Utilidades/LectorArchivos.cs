using System.Collections.Generic;
using System.IO;

namespace biblioteca_hotel.Utilidades
{
    public class LectorArchivos : Interfaces.IGestionHotel
    {
        protected string ruta_archivo;
        protected List<Modelos.Personas.Persona> lista_personas;

        public LectorArchivos(string ruta_archivo)
        {
            this.ruta_archivo = ruta_archivo;
            lista_personas = new List<Modelos.Personas.Persona>();
        }

        public void cargar_datos(string archivo)
        {
            if (File.Exists(archivo))
            {
                var lineas = File.ReadAllLines(archivo);
                foreach (var linea in lineas)
                {
                    procesar_linea(linea);
                }
            }
        }

        protected void procesar_linea(string linea)
        {
            if (!string.IsNullOrEmpty(linea))
            {
                var partes = linea.Split(',');
                if (partes.Length >= 4)
                {
                    var persona = new Modelos.Personas.Huesped(
                        partes[0],
                        partes[1],
                        partes[2],
                        partes[3]
                    );
                    lista_personas.Add(persona);
                }
            }
        }

        public void procesar_factura(Modelos.Habitaciones.Habitacion habitacion)
        {
        }

        public void gestionar_reserva(Modelos.Core.Reserva reserva)
        {
        }

        public Modelos.Personas.Persona[] GetPersonas() => lista_personas.ToArray();
    }
}
