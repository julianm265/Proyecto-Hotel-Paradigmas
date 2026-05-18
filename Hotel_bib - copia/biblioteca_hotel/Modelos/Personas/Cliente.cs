using System;

namespace biblioteca_hotel.Modelos.Personas
{
    public class Cliente : Persona
    {
        protected int código;
        protected float descuento;
        private static Random random = new Random();
        private static object lockObject = new object();

        public Cliente(string nombre, string tipo_id, string numero_documento, string telefono)
            : base(nombre, tipo_id, numero_documento, telefono)
        {
            this.código = GenerarCodigoCliente();
            this.descuento = GenerarDescuentoCliente();
        }

        public Cliente(string nombre, string tipo_id, string numero_documento, string telefono, int codigo, float descuento)
            : base(nombre, tipo_id, numero_documento, telefono)
        {
            if (codigo <= 0)
                throw new ArgumentException("El código del cliente debe ser mayor a 0");

            if (descuento < 0 || descuento > 1)
                throw new ArgumentException("El descuento debe estar entre 0 y 1 (0% a 100%)");

            this.código = codigo;
            this.descuento = descuento;
        }

        /// <summary>
        /// Genera un código de cliente aleatorio único
        /// </summary>
        private static int GenerarCodigoCliente()
        {
            lock (lockObject)
            {
                return random.Next(1000, 999999);
            }
        }

        /// <summary>
        /// Genera un descuento aleatorio entre 0% y 20%
        /// </summary>
        private static float GenerarDescuentoCliente()
        {
            lock (lockObject)
            {
                // Descuento entre 0 y 0.20 (0% a 20%)
                return (float)(random.NextDouble() * 0.20);
            }
        }

        public int GetCodigo() => código;
        public float GetDescuento() => descuento;
    }
}
    