namespace biblioteca_hotel.Modelos.Productos
{
    public class ConsumoMinibar : Consumible
    {
        private readonly string _nombre;

        public ConsumoMinibar(string nombre, decimal costo)
            : base(costo)
        {
            _nombre = nombre;
        }

        public string GetNombreProducto() => _nombre;
    }
}
