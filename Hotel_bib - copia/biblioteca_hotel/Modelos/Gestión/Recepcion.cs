using System;
using System.Collections.Generic;

namespace biblioteca_hotel.Modelos.Gestión
{
    public class Recepcion : Interfaces.IProceso
    {
        public List<Core.Factura> l_facturas;

        public Recepcion()
        {
            l_facturas = new List<Core.Factura>();
        }

        public Core.Factura Check_Out(Personas.Persona persona)
        {
            var factura = new Core.Factura(persona, false);
            l_facturas.Add(factura);
            return factura;
        }

        public void Check_In(Personas.Persona persona)
        {
        }

        public void Check_In(Personas.Persona persona, DateTime fecha_entrada, DateTime fecha_salida)
        {
        }

        public Core.Factura[] GetFacturas() => l_facturas.ToArray();
    }
}
