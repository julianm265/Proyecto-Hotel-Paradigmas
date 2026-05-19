namespace biblioteca_hotel.Modelos.Core
{
    public class Minibar
    {
        protected Enums.TipoMinibar tipo;
        private System.Collections.Generic.Dictionary<string, int> inventario;
        private const int CAPACIDAD_MAXIMA = 10;

        public Minibar(Enums.TipoMinibar tipo)
        {
            this.tipo = tipo;
            inventario = new System.Collections.Generic.Dictionary<string, int>();
        }

        public void LLenar(Enums.TipoMinibar tipo_habitacion)
        {
            InicializarInventario();
            this.tipo = tipo_habitacion;
            var productos = new[] { "Licor", "Vino", "Kit aseo", "Agua", "Gaseosa", "Bata" };

            foreach (var p in productos)
            {
                if (!inventario.ContainsKey(p))
                    inventario[p] = 0;
                
                inventario[p] = CAPACIDAD_MAXIMA;
            }
        }

        public bool Consumir(string producto, int cantidad = 1)
        {
            InicializarInventario();
            if (inventario.ContainsKey(producto) && inventario[producto] >= cantidad)
            {
                inventario[producto] -= cantidad;
                return true;
            }
            return false;
        }

        public bool Reponer(string producto, int cantidad)
        {
            InicializarInventario();
            if (cantidad <= 0) return false;
            if (!inventario.ContainsKey(producto))
                inventario[producto] = 0;

            if (inventario[producto] + cantidad > CAPACIDAD_MAXIMA)
                return false;

            inventario[producto] += cantidad;
            return true;
        }

        public int GetStock(string producto)
        {
            InicializarInventario();
            return inventario.TryGetValue(producto, out var cantidad) ? cantidad : 0;
        }

        private void InicializarInventario()
        {
            inventario ??= new System.Collections.Generic.Dictionary<string, int>();
        }

        public Enums.TipoMinibar GetTipo() => tipo;
        public System.Collections.Generic.Dictionary<string, int> GetInventario() => inventario;
    }
}
