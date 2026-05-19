using biblioteca_hotel.Servicios;

namespace biblioteca_hotel.Modelos.Personas
{
    public class Cliente : Persona
    {
        protected int código;
        protected float descuento;

        public Cliente(string nombre, string tipo_id, string numero_documento, string telefono)
            : base(nombre, tipo_id, numero_documento, telefono)
        {
            this.código = PersonaService.GenerarCodigoCliente();
            this.descuento = PersonaService.GenerarDescuentoSemanal();
        }

        public Cliente(string nombre, string tipo_id, string numero_documento, string telefono, int codigo, float descuento)
            : base(nombre, tipo_id, numero_documento, telefono)
        {
            this.código = codigo;
            this.descuento = descuento;
        }

        public int GetCodigo() => código;
        public float GetDescuento() => descuento;
    }
}
