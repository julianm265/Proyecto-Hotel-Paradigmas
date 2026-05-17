namespace biblioteca_hotel.Modelos.Productos
{
    public abstract class Producto
    {
        public decimal costo;

        public Producto(decimal costo)
        {
            this.costo = costo;
        }

        public decimal GetCosto() => costo;
    }
}
