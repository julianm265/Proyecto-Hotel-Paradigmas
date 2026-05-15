namespace biblioteca_hotel.Modelos.Productos
{
    public class Agua : Botella
    {
        public Agua(decimal costo)
            : base(costo)
        {
        }

        public override string Mostrar_cod(Producto producto)
        {
            return $"Agua - {base.Mostrar_cod(producto)}";
        }
    }
}
