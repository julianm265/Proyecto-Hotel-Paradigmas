namespace biblioteca_hotel.Modelos.Habitaciones
{
    /// <summary>
    /// Habitación sencilla con una cama y costo definido en reglas de negocio.
    /// El costo se establece automáticamente de ReglasNegocioHabitaciones.costo_noche_hab_sencilla.
    /// </summary>
    public class Sencilla : Habitacion
    {
        /// <summary>
        /// Constructor único que valida el número de habitación.
        /// El costo se obtiene automáticamente de las reglas de negocio.
        /// </summary>
        public Sencilla(string numero_habitacion, string tipo_cama, int piso)
            : base(Utilidades.ReglasNegocioHabitaciones.costo_noche_hab_sencilla)
        {
            ValidarNumeroHabitacion(numero_habitacion, piso);
            num_hab = numero_habitacion;
            l_camas = new Camas.Cama[] { CrearCama(tipo_cama) };
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
    }
}
