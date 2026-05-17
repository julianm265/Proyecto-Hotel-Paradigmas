using biblioteca_hotel.Modelos.Core;
using biblioteca_hotel.Modelos.Habitaciones;
using biblioteca_hotel.Modelos.Personas;
using biblioteca_hotel.Modelos.Productos;
using biblioteca_hotel.Modelos.Servicios;
using biblioteca_hotel.Modelos.Carta;
using biblioteca_hotel.Utilidades;
using biblioteca_hotel.Servicios;

namespace Front_hotel.Services
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider services)
        {
            var personas = services.GetRequiredService<PersonaService>();
            var habitaciones = services.GetRequiredService<HabitacionService>();
            var reservas = services.GetRequiredService<ReservaService>();
            var facturas = services.GetRequiredService<FacturaService>();
            var carta = services.GetRequiredService<CartaService>();

            if (personas.ObtenerTodos().Length > 0)
                return;

            var cliente = new Cliente("Ana Torres", "DNI", "123456", "3124567890", 1201, 0.1f);
            var huesped = new Huesped("Luis Paredes", "PASAPORTE", "A92133", "3107778899");
            var huesped2 = new Huesped("Marina Díaz", "CE", "445566", "3205547788");

            personas.Agregar(cliente);
            personas.Agregar(huesped);
            personas.Agregar(huesped2);

            var habSencilla = new Sencilla(ReglasNegocioHabitaciones.costo_noche_hab_sencilla, "queen", 1);
            var habEjecutiva = new Ejecutiva(ReglasNegocioHabitaciones.costo_noche_hab_ejecutiva, "king");
            var habSuite = new Suite(ReglasNegocioHabitaciones.costo_noche_suite, "king");

            habitaciones.Agregar(habSencilla);
            habitaciones.Agregar(habEjecutiva);
            habitaciones.Agregar(habSuite);

            reservas.Agregar(new Reserva(cliente, habSencilla, DateTime.Today, DateTime.Today.AddDays(3)));
            reservas.Agregar(new Reserva(huesped, habEjecutiva, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            var factura = new Factura(cliente, true);
            factura.AgregarProducto(new Agua(ReglasNegocioHabitaciones.precio_agua));
            factura.AgregarProducto(new Vino(ReglasNegocioHabitaciones.precio_vino));
            factura.AgregarProducto(new Kit_aseo(ReglasNegocioHabitaciones.precio_kit_aseo));
            factura.AgregarServicio(new Servicio_Hotelero(new Restaurante(), new Lavandera()));
            facturas.Agregar(factura);

            carta.Agregar(new Desayuno(ReglasNegocioServicios.Costo_desayuno));
            carta.Agregar(new Almuerzo(ReglasNegocioServicios.Costo_almuerzo));
            carta.Agregar(new Cena(ReglasNegocioServicios.costo_cena));
        }
    }
}
