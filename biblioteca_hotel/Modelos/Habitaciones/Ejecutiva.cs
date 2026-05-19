namespace biblioteca_hotel.Modelos.Habitaciones
{
    /// <summary>
    /// Habitación ejecutiva con minibar y costo definido en reglas de negocio.
    /// El costo se establece automáticamente de ReglasNegocioHabitaciones.costo_noche_hab_ejecutiva.
    /// </summary>
    public class Ejecutiva : Habitacion, Interfaces.Isurtidor
    {
        protected Core.Minibar minibar;

        /// <summary>
        /// Constructor único que valida el número de habitación.
        /// El costo se obtiene automáticamente de las reglas de negocio.
        /// </summary>
        public Ejecutiva(string numero_habitacion, string tipo_cama, int piso)
            : base(Utilidades.ReglasNegocioHabitaciones.costo_noche_hab_ejecutiva)
        {
            ValidarNumeroHabitacion(numero_habitacion, piso);
            num_hab = numero_habitacion;
            minibar = new Core.Minibar(Enums.TipoMinibar.Ejecutivo);
            l_camas = new Camas.Cama[] { CrearCama(tipo_cama) };
        }

        public void LLenar(Enums.TipoMinibar tipo_habitacion)
        {
            minibar.LLenar(tipo_habitacion);
        }

        public void Llenar(Core.Minibar minibar)
        {
            this.minibar = minibar;
        }

        private Camas.Cama CrearCama(string tipo)
        {
            return tipo?.ToLower() switch
            {
                "doble" => new Camas.Doble(),
                "semidoble" => new Camas.Semidoble(),
                "queen" => new Camas.Queen(),
                "king" => new Camas.King(),
                _ => new Camas.Sencilla()
            };
        }

        public Core.Minibar GetMinibar() => minibar;
    }
}
