namespace biblioteca_hotel.Eventos
{
    public class EventoCancelacionReserva : EventoBase
    {
        public string idReserva;
        public string idCliente;

        public EventoCancelacionReserva(string id_reserva, string id_cliente)
            : base(Enums.TipoEvento.CancelacionReserva)
        {
            idReserva = id_reserva;
            idCliente = id_cliente;
            if (gestor != null)
            {
                gestor.notificar(this);
            }
        }

        public float calcularPenalizacion()
        {
            return (float)Utilidades.ReglasNegocioHabitaciones.costo_noche_hab_sencilla * 0.1f;
        }

        public void liberarHabitacion()
        {
        }
    }
}
