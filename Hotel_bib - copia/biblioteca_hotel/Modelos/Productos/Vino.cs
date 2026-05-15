namespace biblioteca_hotel.Modelos.Productos
{
    public class Vino : Botella
    {
        public Vino(decimal costo)
            : base(costo)
        {
        }

        public override string Mostrar_cod(Producto producto)
        {
            return $"Vino - {base.Mostrar_cod(producto)}";
        }
    }
}
