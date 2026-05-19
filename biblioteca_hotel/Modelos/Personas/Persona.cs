using System;
using System.Text.RegularExpressions;

namespace biblioteca_hotel.Modelos.Personas
{
    /// <summary>
    /// Clase base abstracta para personas (clientes, huéspedes) con validaciones de datos.
    /// </summary>
    public abstract class Persona
    {
        protected string nombre;
        protected string id;
        protected Enums.TipoId tipo_id;
        protected string número_telefono;

        // Regex para validaciones
        private static readonly Regex REGEX_NOMBRE = new Regex(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ\s]+$", RegexOptions.Compiled);
        private static readonly Regex REGEX_DOCUMENTO_NUMERICO = new Regex(@"^\d+$", RegexOptions.Compiled);
        private static readonly Regex REGEX_DOCUMENTO_ALFABETICO = new Regex(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        private static readonly Regex REGEX_TELEFONO = new Regex(@"^\d{10,}$", RegexOptions.Compiled);

        public Persona(string nombre, string tipo_id, string numero_documento, string telefono)
        {
            ValidarNombre(nombre);
            Enums.TipoId tipoIdParsed = ParseTipoId(tipo_id);
            ValidarDocumento(numero_documento, tipoIdParsed);
            ValidarTelefono(telefono);

            this.nombre = nombre;
            this.id = numero_documento;
            this.tipo_id = tipoIdParsed;
            this.número_telefono = telefono;
        }

        /// <summary>
        /// Valida que el nombre solo contenga letras y espacios (sin números ni caracteres especiales).
        /// </summary>
        private void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (!REGEX_NOMBRE.IsMatch(nombre))
                throw new ArgumentException("El nombre solo puede contener letras y espacios.");

            if (nombre.Length > 100)
                throw new ArgumentException("El nombre no puede exceder 100 caracteres.");
        }

        /// <summary>
        /// Valida el documento según el tipo:
        /// - DNI, NIT: Solo números
        /// - Pasaporte, CE: Números y letras
        /// </summary>
        private void ValidarDocumento(string numero_documento, Enums.TipoId tipo)
        {
            if (string.IsNullOrWhiteSpace(numero_documento))
                throw new ArgumentException("El número de documento no puede estar vacío.");

            // DNI y NIT deben ser solo números
            if (tipo == Enums.TipoId.DNI || tipo == Enums.TipoId.NIT)
            {
                if (!REGEX_DOCUMENTO_NUMERICO.IsMatch(numero_documento))
                    throw new ArgumentException($"El documento {tipo} debe contener solo números.");
            }
            // Pasaporte y CE pueden contener números y letras
            else if (tipo == Enums.TipoId.Pasaporte || tipo == Enums.TipoId.CE)
            {
                if (!REGEX_DOCUMENTO_ALFABETICO.IsMatch(numero_documento))
                    throw new ArgumentException($"El documento {tipo} solo puede contener números y letras.");
            }

            if (numero_documento.Length < 5 || numero_documento.Length > 20)
                throw new ArgumentException("El documento debe tener entre 5 y 20 caracteres.");
        }

        /// <summary>
        /// Valida que el teléfono sea válido:
        /// - Mínimo 10 dígitos
        /// - No puede ser 0
        /// - Solo números
        /// </summary>
        private void ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede estar vacío.");

            if (!REGEX_TELEFONO.IsMatch(telefono))
                throw new ArgumentException("El teléfono debe tener mínimo 10 dígitos.");

            if (long.TryParse(telefono, out long telefonoNumero))
            {
                if (telefonoNumero == 0)
                    throw new ArgumentException("El teléfono no puede ser 0.");
            }
            else
            {
                throw new ArgumentException("El teléfono debe ser un número válido.");
            }
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
        public string GetTelefono() => número_telefono;
    }
}
