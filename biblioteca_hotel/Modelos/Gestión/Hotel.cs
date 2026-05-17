using System.Collections.Generic;

namespace biblioteca_hotel.Modelos.Gestión
{
    public class Hotel
    {
        protected Oficina oficina;
        protected Recepcion recepcion;
        protected Servicios.Servicio_Hotelero servicio_hotelero;
        protected List<Core.Piso> l_piso;

        public Hotel(Recepcion recepcion, Oficina oficina)
        {
            this.recepcion = recepcion;
            this.oficina = oficina;
            l_piso = new List<Core.Piso>();
            servicio_hotelero = new Servicios.Servicio_Hotelero(
                new Servicios.Restaurante(),
                new Servicios.Lavandera()
            );
        }

        public void AgregarPiso(Core.Piso piso) => l_piso.Add(piso);

        public Oficina GetOficina() => oficina;
        public Recepcion GetRecepcion() => recepcion;
        public Servicios.Servicio_Hotelero GetServicioHotelero() => servicio_hotelero;
        public Core.Piso[] GetPisos() => l_piso.ToArray();
    }
}
