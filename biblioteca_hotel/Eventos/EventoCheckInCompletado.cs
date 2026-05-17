namespace biblioteca_hotel.Eventos
{
    public class EventoCheckInCompletado : EventoBase
    {
        public string idHuesped;
        public Modelos.Habitaciones.Habitacion habitacion;

        public EventoCheckInCompletado(string id_huesped, Modelos.Habitaciones.Habitacion hab)
            : base(Enums.TipoEvento.CheckInCompletado)
        {
            idHuesped = id_huesped;
            habitacion = hab;
            if (gestor != null)
            {
                gestor.notificar(this);
            }
        }

        public void asignarHabitacion()
        {
        }

        public void activarServicios()
        {
        }
    }
}
