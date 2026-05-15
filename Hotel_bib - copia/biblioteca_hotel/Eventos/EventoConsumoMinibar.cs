namespace biblioteca_hotel.Eventos
{
    public class EventoConsumoMinibar : EventoBase
    {
        public int idHabitacion;
        public int cantidad;

        public EventoConsumoMinibar(int id_habitacion, int cantidad)
            : base(Enums.TipoEvento.ConsumoMinibar)
        {
            idHabitacion = id_habitacion;
            this.cantidad = cantidad;
            if (gestor != null)
            {
                gestor.notificar(this);
            }
        }

        public void registrarConsumo()
        {
        }

        public decimal calcularCostoConsumo()
        {
            return Utilidades.ReglasNegocioHabitaciones.precio_licor * cantidad;
        }
    }
}
