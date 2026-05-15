namespace biblioteca_hotel.Modelos.Productos
{
    public class Kit_aseo : Consumible
    {
        public Kit_aseo(decimal costo)
            : base(costo)
        {
        }

        public override string Mostrar_cod(Producto producto)
        {
            return $"Kit Aseo - {base.Mostrar_cod(producto)}";
        }
    }
}
