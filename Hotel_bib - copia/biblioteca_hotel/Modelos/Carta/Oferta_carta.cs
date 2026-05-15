namespace biblioteca_hotel.Modelos.Carta
{
    public abstract class Oferta_carta
    {
        protected decimal Costo;

        public Oferta_carta(decimal costo)
        {
            Costo = costo;
        }

        public decimal GetCosto() => Costo;
    }
}
