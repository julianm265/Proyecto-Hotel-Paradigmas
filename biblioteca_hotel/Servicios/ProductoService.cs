using System.Collections.Generic;
using System.Linq;

namespace biblioteca_hotel.Servicios
{
    public class ProductoService
    {
        protected List<Modelos.Productos.Producto> producto;

        public ProductoService()
        {
            producto = new List<Modelos.Productos.Producto>();
        }

        public void Agregar(Modelos.Productos.Producto prod)
        {
            producto.Add(prod);
        }

        public Modelos.Productos.Producto[] ObtenerTodos()
        {
            return producto.ToArray();
        }

        public Modelos.Productos.Producto BuscarPorCodigo(int codigo)
        {
            return producto.ElementAtOrDefault(codigo);
        }

        public bool Existe(int codigo)
        {
            return codigo >= 0 && codigo < producto.Count;
        }
    }
}
