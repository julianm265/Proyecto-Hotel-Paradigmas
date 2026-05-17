namespace biblioteca_hotel.Interfaces
{
    public interface IProductoMinibar
    {
        string Mostrar_cod(Modelos.Productos.Producto producto);
        decimal ObtenerPrecioActual(string id_producto);
    }
}
