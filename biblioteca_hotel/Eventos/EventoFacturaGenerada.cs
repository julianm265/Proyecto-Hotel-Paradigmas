namespace biblioteca_hotel.Eventos
{
    public class EventoFacturaGenerada : EventoBase
    {
        public float total;

        public EventoFacturaGenerada(string id_factura, decimal monto)
            : base(Enums.TipoEvento.FacturaGenerada)
        {
            total = (float)monto;
            if (gestor != null)
            {
                gestor.notificar(this);
            }
        }

        public float calcularImpuestos()
        {
            return total * Utilidades.Regla_negocio.valor_IVA;
        }

        public void generarPDF()
        {
        }
    }
}
