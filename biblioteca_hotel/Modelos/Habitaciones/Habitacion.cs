using System;

namespace biblioteca_hotel.Modelos.Habitaciones
{
    /// <summary>
    /// Clase base abstracta para todas las habitaciones del hotel.
    /// Define propiedades comunes y validaciones de número de habitación.
    /// </summary>
    public abstract class Habitacion
    {
        protected Camas.Cama[] l_camas;
        protected decimal Costo_noche;
        protected string num_hab;

        /// <summary>
        /// Constructor base de habitación con costo por noche.
        /// </summary>
        public Habitacion(decimal costo_noche)
        {
            Costo_noche = costo_noche;
            l_camas = new Camas.Cama[0];
            num_hab = string.Empty;
        }

        /// <summary>
        /// Valida el número de habitación según coherencia con el piso.
        /// Reglas:
        /// - No puede estar vacío o nulo
        /// - Debe ser un número válido entre 100 y 9999
        /// - Los dígitos iniciales deben coincidir con el piso
        /// - Los últimos 2 dígitos (unidad) deben estar entre 01 y 99
        /// </summary>
        protected void ValidarNumeroHabitacion(string numero, int piso)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("El número de habitación no puede estar vacío o nulo", nameof(numero));

            if (!int.TryParse(numero, out int numHab))
                throw new ArgumentException("El número de habitación debe ser un valor numérico válido", nameof(numero));

            if (numHab < 100 || numHab > 9999)
                throw new ArgumentException("El número de habitación debe estar entre 100 y 9999", nameof(numero));

            // Obtener el primer dígito o dos dígitos según sea piso de 1 dígito o 2 dígitos
            int pisoDelNumero = numHab / 100;

            if (pisoDelNumero != piso)
                throw new ArgumentException($"El número de habitación {numero} no corresponde al piso {piso}. " +
                    $"El piso indicado en el número es {pisoDelNumero}", nameof(numero));

            // Validar que la unidad (últimos 2 dígitos) esté entre 01 y 99
            int unidad = numHab % 100;
            if (unidad < 1 || unidad > 99)
                throw new ArgumentException("El número de habitación debe terminar con unidad entre 01 y 99", nameof(numero));
        }

        public virtual double calcularCosto()
        {
            return (double)(Costo_noche * l_camas.Length);
        }

        public decimal GetCostoNoche() => Costo_noche;
        public string GetNumHab() => num_hab;
        public Camas.Cama[] GetCamas() => l_camas;
    }
}
