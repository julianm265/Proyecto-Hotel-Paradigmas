namespace biblioteca_hotel.Modelos.Productos
{
    public class Gaseosa : Consumible
    {
        public Gaseosa(decimal costo)
            : base(costo)
        {
        }

        public override string Mostrar_cod(Producto producto)
        {
            return $"Gaseosa - {base.Mostrar_cod(producto)}";
        }
    }
}
