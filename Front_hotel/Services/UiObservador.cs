using biblioteca_hotel.Enums;
using biblioteca_hotel.Interfaces;

namespace Front_hotel.Services
{
    public class UiObservador : IObservador
    {
        private readonly NotificationService _notifications;

        public UiObservador(NotificationService notifications)
        {
            _notifications = notifications;
        }

        public void actualizar(TipoEvento evento)
        {
            _notifications.AddFromEvent(evento);
        }
    }
}
