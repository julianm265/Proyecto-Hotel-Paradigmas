using System.Collections.Generic;

namespace biblioteca_hotel.Eventos
{
    public class GestorNotificaciones
    {
        public List<Interfaces.IObservador> observadores;

        public GestorNotificaciones()
        {
            observadores = new List<Interfaces.IObservador>();
        }

        public void suscribir(Interfaces.IObservador observador)
        {
            observadores.Add(observador);
        }

        public void desuscribir(Interfaces.IObservador observador)
        {
            observadores.Remove(observador);
        }

        public void notificar(EventoBase evento)
        {
            foreach (var observador in observadores)
            {
                observador.actualizar(evento.GetTipoEvento());
            }
        }
    }
}
