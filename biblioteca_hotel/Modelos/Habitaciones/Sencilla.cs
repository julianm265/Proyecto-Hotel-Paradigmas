namespace biblioteca_hotel.Modelos.Habitaciones
{
    public class Sencilla : Habitacion
    {
        public Sencilla(decimal costo_noche, string tipo_cama, int piso)
            : base(costo_noche)
        {
            num_hab = $"{piso}01";
            l_camas = new Camas.Cama[] { CrearCama(tipo_cama) };
        }

        public Sencilla(decimal costo_noche, string tipo_cama, string numHab)
            : base(costo_noche)
        {
            num_hab = numHab;
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
