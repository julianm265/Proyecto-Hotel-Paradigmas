namespace biblioteca_hotel.Modelos.Productos
{
    public abstract class Botella : Consumible
    {
        public Botella(decimal costo)
            : base(costo)
        {
        }

        public override string Mostrar_cod(Producto producto)
        {
            return base.Mostrar_cod(producto);
        }
    }
}
