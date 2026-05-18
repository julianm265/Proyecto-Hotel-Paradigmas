using biblioteca_hotel.Utilidades;

namespace biblioteca_hotel.Modelos.Habitaciones
{
    public class Suite : Habitacion, Interfaces.Isurtidor
    {
        protected Core.Minibar minibar;

        public Suite(string numero_habitacion, string tipo_cama, int piso)
            : base(ReglasNegocioHabitaciones.costo_noche_suite)
        {
            ValidarNumeroHabitacion(numero_habitacion, piso);
            num_hab = numero_habitacion;
            minibar = new Core.Minibar(Enums.TipoMinibar.Suite);
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
