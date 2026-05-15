using System;
using System.Text.RegularExpressions;
using biblioteca_hotel.Utilidades;

namespace biblioteca_hotel.Aspectos
{
    public class Validador
    {
        public string pointcut_guardar()
        {
            return "Pointcut: guardar(Reserva) | guardar(Factura) | guardar(Persona)";
        }

        public void Advice_ValidarYPersistir(object obj)
        {
            if (obj == null)
            {
                mostrar_mensaje(false);
                return;
            }

            bool valido = false;

            if (obj is Modelos.Personas.Persona persona)
            {
                valido = ValidarPersona(persona);
            }
            else if (obj is Modelos.Core.Reserva reserva)
            {
                valido = ValidarReserva(reserva);
            }
            else if (obj is Modelos.Core.Factura factura)
            {
                valido = ValidarFactura(factura);
            }

            mostrar_mensaje(valido);
        }

        private bool ValidarPersona(Modelos.Personas.Persona persona)
        {
            var regexId = new Regex(Regla_negocio.regex_id);
            var regexTel = new Regex(Regla_negocio.regex_telefono);

            return regexId.IsMatch(persona.GetId()) && regexTel.IsMatch(persona.GetTelefono().ToString());
        }

        private bool ValidarReserva(Modelos.Core.Reserva reserva)
        {
            return reserva.GetFechaSalida() > reserva.GetFechaEntrada();
        }

        private bool ValidarFactura(Modelos.Core.Factura factura)
        {
            return factura.calcularTotal() > 0;
        }

        protected void mostrar_mensaje(bool valido)
        {
            string mensaje = valido ? "Validación exitosa" : "Validación fallida";
            System.Diagnostics.Debug.WriteLine(mensaje);
        }

        public void cargar_datos()
        {
        }
    }
}
