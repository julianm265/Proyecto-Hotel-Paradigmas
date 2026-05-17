namespace biblioteca_hotel.Modelos.Core
{
    public class Minibar
    {
        protected Enums.TipoMinibar tipo;

        public Minibar(Enums.TipoMinibar tipo)
        {
            this.tipo = tipo;
        }

        public void LLenar(Enums.TipoMinibar tipo_habitacion)
        {
            this.tipo = tipo_habitacion;
        }

        public Enums.TipoMinibar GetTipo() => tipo;
    }
}
