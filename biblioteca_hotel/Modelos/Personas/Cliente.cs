using biblioteca_hotel.Servicios;

namespace biblioteca_hotel.Modelos.Personas
{
    /// <summary>
    /// Representa un cliente del hotel con código único y descuento semanal.
    /// El código se genera aleatoriamente y el descuento varía según la semana actual.
    /// </summary>
    public class Cliente : Persona
    {
        protected int código;
        protected float descuento;

        /// <summary>
        /// Constructor con auto-generación de código y descuento semanal.
        /// </summary>
        public Cliente(string nombre, string tipo_id, string numero_documento, string telefono)
            : base(nombre, tipo_id, numero_documento, telefono)
        {
            this.código = PersonaService.GenerarCodigoCliente();
            this.descuento = PersonaService.GenerarDescuentoSemanal();
        }

        /// <summary>
        /// Constructor con valores manuales de código y descuento.
        /// </summary>
        public Cliente(string nombre, string tipo_id, string numero_documento, string telefono, int codigo, float descuento)
            : base(nombre, tipo_id, numero_documento, telefono)
        {
            if (codigo <= 0)
                throw new System.ArgumentException("El código del cliente debe ser mayor a 0");

            if (descuento < 0 || descuento > 1)
                throw new System.ArgumentException("El descuento debe estar entre 0 y 1 (0% a 100%)");

            this.código = codigo;
            this.descuento = descuento;
        }

        /// <summary>
        /// Actualiza el descuento a un nuevo valor según la semana actual.
        /// Útil cuando el cliente es consultado en una nueva semana.
        /// </summary>
        public void ActualizarDescuentoSemanal()
        {
            this.descuento = PersonaService.GenerarDescuentoSemanal();
        }

        public int GetCodigo() => código;
        public float GetDescuento() => descuento;
    }
}
