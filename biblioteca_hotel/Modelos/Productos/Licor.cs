namespace biblioteca_hotel.Modelos.Productos
{
    public class Licor : Botella
    {
        public Licor(decimal costo)
            : base(costo)
        {
        }

        public override string Mostrar_cod(Producto producto)
        {
            return $"Licor - {base.Mostrar_cod(producto)}";
        }
    }
}
