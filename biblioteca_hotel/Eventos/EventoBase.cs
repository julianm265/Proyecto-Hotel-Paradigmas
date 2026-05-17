using System;

namespace biblioteca_hotel.Eventos
{
    public abstract class EventoBase
    {
        public string idEvento;
        protected Enums.TipoEvento tipoEvento;
        protected static GestorNotificaciones gestor;

        public EventoBase(Enums.TipoEvento tipo)
        {
            idEvento = Guid.NewGuid().ToString();
            tipoEvento = tipo;
        }

        public static void SetGestor(GestorNotificaciones g)
        {
            gestor = g;
        }

        public Enums.TipoEvento GetTipoEvento() => tipoEvento;
        public string GetIdEvento() => idEvento;
    }
}
