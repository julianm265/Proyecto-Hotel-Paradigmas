namespace biblioteca_hotel.Modelos.Productos
{
    /// <summary>
    /// Producto genérico para registrar consumos del restaurante (Desayuno, Almuerzo, Cena)
    /// en la factura activa del huésped vía FacturaService.ActualizarConsumo().
    /// </summary>
    public class ProductoRestaurante : Producto
    {
        private readonly string _nombre;

        public ProductoRestaurante(string nombre, decimal costo) : base(costo)
        {
            _nombre = nombre;
        }

        public string GetNombreProducto() => _nombre;
    }
}
