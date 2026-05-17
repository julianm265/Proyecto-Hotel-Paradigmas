namespace biblioteca_hotel.Modelos.Productos
{
    public abstract class Consumible : Producto, Interfaces.IProductoMinibar
    {
        public Consumible(decimal costo)
            : base(costo)
        {
        }

        public virtual string Mostrar_cod(Producto producto)
        {
            return $"Código: {GetHashCode()}";
        }

        public virtual decimal ObtenerPrecioActual(string id_producto)
        {
            return costo;
        }
    }
}
