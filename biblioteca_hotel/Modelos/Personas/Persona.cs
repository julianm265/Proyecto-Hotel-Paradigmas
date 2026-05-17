using System.Text.RegularExpressions;

namespace biblioteca_hotel.Modelos.Personas
{
    public abstract class Persona
    {
        protected string nombre;
        protected string id;
        protected Enums.TipoId tipo_id;
        protected int número_telefono;

        public Persona(string nombre, string tipo_id, string numero_documento, string telefono)
        {
            this.nombre = nombre;
            this.id = numero_documento;
            this.tipo_id = ParseTipoId(tipo_id);
            this.número_telefono = int.TryParse(telefono, out int tel) ? tel : 0;
        }

        private Enums.TipoId ParseTipoId(string tipo)
        {
            return tipo?.ToUpper() switch
            {
                "DNI" => Enums.TipoId.DNI,
                "PASAPORTE" => Enums.TipoId.Pasaporte,
                "CE" => Enums.TipoId.CE,
                "NIT" => Enums.TipoId.NIT,
                _ => Enums.TipoId.DNI
            };
        }

        public string GetNombre() => nombre;
        public string GetId() => id;
        public Enums.TipoId GetTipoId() => tipo_id;
        public int GetTelefono() => número_telefono;
    }
}
