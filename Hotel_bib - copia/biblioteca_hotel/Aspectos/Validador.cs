using System;
using System.Text.RegularExpressions;
using biblioteca_hotel.Utilidades;

namespace biblioteca_hotel.Aspectos
{
    /// <summary>
    /// Clase Validador que implementa un aspecto de validación para objetos clave del sistema.
    /// Valida Personas, Reservas y Facturas con reglas de negocio específicas.
    /// </summary>
    public class Validador
    {
        private int validaciones_exitosas = 0;
        private int validaciones_fallidas = 0;

        public string pointcut_guardar()
        {
            return "Pointcut: guardar(Reserva) | guardar(Factura) | guardar(Persona)";
        }

        /// <summary>
        /// Valida y persiste un objeto según su tipo
        /// </summary>
        /// <param name="obj">Objeto a validar</param>
        /// <returns>true si la validación fue exitosa, false en caso contrario</returns>
        /// <exception cref="ArgumentNullException">Si el objeto es nulo</exception>
        public bool Advice_ValidarYPersistir(object obj)
        {
            try
            {
                if (obj == null)
                {
                    mostrar_mensaje(false, "Objeto nulo recibido en validación");
                    validaciones_fallidas++;
                    return false;
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
                else
                {
                    mostrar_mensaje(false, $"Tipo de objeto no reconocido: {obj.GetType().Name}");
                    validaciones_fallidas++;
                    return false;
                }

                if (valido)
                    validaciones_exitosas++;
                else
                    validaciones_fallidas++;

                mostrar_mensaje(valido, valido ? $"{obj.GetType().Name} validado correctamente" : 
                                $"{obj.GetType().Name} no cumple las reglas de validación");

                return valido;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en validación: {ex.Message}");
                mostrar_mensaje(false, $"Error durante la validación: {ex.Message}");
                validaciones_fallidas++;
                return false;
            }
        }

        /// <summary>
        /// Valida un objeto Persona según reglas de negocio
        /// </summary>
        private bool ValidarPersona(Modelos.Personas.Persona persona)
        {
            try
            {
                if (persona == null)
                {
                    mostrar_mensaje(false, "Persona nula");
                    return false;
                }

                // Validar que los datos esenciales no sean nulos
                if (string.IsNullOrWhiteSpace(persona.GetNombre()))
                {
                    mostrar_mensaje(false, "Nombre de persona vacío");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(persona.GetId()))
                {
                    mostrar_mensaje(false, "ID de persona vacío");
                    return false;
                }

                // Validar formato de ID con regex
                try
                {
                    var regexId = new Regex(Regla_negocio.regex_id);
                    if (!regexId.IsMatch(persona.GetId()))
                    {
                        mostrar_mensaje(false, $"Formato de ID inválido: {persona.GetId()}. Debe ser entre 6 y 10 dígitos");
                        return false;
                    }
                }
                catch (RegexMatchTimeoutException ex)
                {
                    mostrar_mensaje(false, $"Timeout en validación de ID: {ex.Message}");
                    return false;
                }

                // Validar formato de teléfono con regex
                try
                {
                    var regexTel = new Regex(Regla_negocio.regex_telefono);
                    string telefonoStr = persona.GetTelefono().ToString();

                    if (!regexTel.IsMatch(telefonoStr))
                    {
                        mostrar_mensaje(false, $"Formato de teléfono inválido: {telefonoStr}. Debe ser entre 7 y 10 dígitos");
                        return false;
                    }
                }
                catch (RegexMatchTimeoutException ex)
                {
                    mostrar_mensaje(false, $"Timeout en validación de teléfono: {ex.Message}");
                    return false;
                }
                catch (FormatException ex)
                {
                    mostrar_mensaje(false, $"Error en formato de teléfono: {ex.Message}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validando Persona: {ex.Message}");
                mostrar_mensaje(false, $"Error inesperado validando Persona: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Valida un objeto Reserva según reglas de negocio
        /// </summary>
        private bool ValidarReserva(Modelos.Core.Reserva reserva)
        {
            try
            {
                if (reserva == null)
                {
                    mostrar_mensaje(false, "Reserva nula");
                    return false;
                }

                // Validar que la persona asignada no sea nula
                if (reserva.GetPersona() == null)
                {
                    mostrar_mensaje(false, "Reserva sin persona asignada");
                    return false;
                }

                // Validar que la habitación asignada no sea nula
                if (reserva.GetHabitacion() == null)
                {
                    mostrar_mensaje(false, "Reserva sin habitación asignada");
                    return false;
                }

                DateTime fechaEntrada = reserva.GetFechaEntrada();
                DateTime fechaSalida = reserva.GetFechaSalida();

                // Validar que la fecha de salida sea posterior a la de entrada
                if (fechaSalida <= fechaEntrada)
                {
                    mostrar_mensaje(false, 
                        $"Fecha de salida ({fechaSalida:yyyy-MM-dd}) debe ser posterior a entrada ({fechaEntrada:yyyy-MM-dd})");
                    return false;
                }

                // Validar que las fechas no sean en el pasado (opcional pero recomendado)
                if (fechaEntrada < DateTime.Now.Date)
                {
                    mostrar_mensaje(false, $"Fecha de entrada no puede ser en el pasado");
                    return false;
                }

                // Validar duración máxima de estadía (ej: máximo 30 días)
                TimeSpan duracion = fechaSalida - fechaEntrada;
                if (duracion.TotalDays > 30)
                {
                    mostrar_mensaje(false, 
                        $"Duración de estadía demasiado larga ({duracion.TotalDays} días). Máximo: 30 días");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validando Reserva: {ex.Message}");
                mostrar_mensaje(false, $"Error inesperado validando Reserva: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Valida un objeto Factura según reglas de negocio
        /// </summary>
        private bool ValidarFactura(Modelos.Core.Factura factura)
        {
            try
            {
                if (factura == null)
                {
                    mostrar_mensaje(false, "Factura nula");
                    return false;
                }

                // Validar que la persona esté asociada
                if (factura.GetPersona() == null)
                {
                    mostrar_mensaje(false, "Factura sin persona asociada");
                    return false;
                }

                // Calcular el total con manejo de excepciones
                decimal total;
                try
                {
                    total = factura.calcularTotal();
                }
                catch (Exception ex)
                {
                    mostrar_mensaje(false, $"Error calculando total de factura: {ex.Message}");
                    return false;
                }

                // Validar que el total sea positivo
                if (total <= 0)
                {
                    mostrar_mensaje(false, $"Total de factura debe ser mayor a cero (actual: {total})");
                    return false;
                }

                // Validar que el total no sea excesivamente grande (límite de negocio)
                const decimal MAX_VALOR_FACTURA = 100000000; // 100 millones
                if (total > MAX_VALOR_FACTURA)
                {
                    mostrar_mensaje(false, 
                        $"Total de factura excede el límite permitido ({total} > {MAX_VALOR_FACTURA})");
                    return false;
                }

                // Validar subtotal
                try
                {
                    decimal subtotal = factura.CalcularSubtotal();
                    if (subtotal < 0)
                    {
                        mostrar_mensaje(false, $"Subtotal no puede ser negativo");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    mostrar_mensaje(false, $"Error validando subtotal: {ex.Message}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validando Factura: {ex.Message}");
                mostrar_mensaje(false, $"Error inesperado validando Factura: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Muestra un mensaje de validación con contexto adicional
        /// </summary>
        protected void mostrar_mensaje(bool valido, string detalles = "")
        {
            try
            {
                string estado = valido ? "✓ EXITOSA" : "✗ FALLIDA";
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string mensaje = $"[{timestamp}] Validación {estado}";
                if (!string.IsNullOrWhiteSpace(detalles))
                    mensaje += $". Detalles: {detalles}";

                System.Diagnostics.Debug.WriteLine(mensaje);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mostrando mensaje: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga datos adicionales si es necesario
        /// </summary>
        public void cargar_datos()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Cargando datos de validación...");
                // Implementar carga de datos de configuración si es necesaria
                System.Diagnostics.Debug.WriteLine("Datos de validación cargados correctamente");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando datos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene estadísticas de validaciones realizadas
        /// </summary>
        public (int exitosas, int fallidas, float tasa_exito) obtener_estadisticas()
        {
            try
            {
                int total = validaciones_exitosas + validaciones_fallidas;
                float tasa_exito = total > 0 ? (validaciones_exitosas / (float)total) * 100 : 0;
                return (validaciones_exitosas, validaciones_fallidas, tasa_exito);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error obteniendo estadísticas: {ex.Message}");
                return (0, 0, 0);
            }
        }

        /// <summary>
        /// Reinicia las estadísticas de validación
        /// </summary>
        public void reiniciar_estadisticas()
        {
            try
            {
                validaciones_exitosas = 0;
                validaciones_fallidas = 0;
                System.Diagnostics.Debug.WriteLine("Estadísticas de validación reiniciadas");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reiniciando estadísticas: {ex.Message}");
            }
        }
    }
}
