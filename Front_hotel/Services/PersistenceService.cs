using System.IO;
using Newtonsoft.Json;
using biblioteca_hotel.Servicios;
using biblioteca_hotel.Modelos.Personas;
using biblioteca_hotel.Modelos.Habitaciones;
using biblioteca_hotel.Modelos.Core;
using biblioteca_hotel.Modelos.Carta;

using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Front_hotel.Services
{
    public class AllFieldsContractResolver : DefaultContractResolver
    {
        protected override System.Collections.Generic.List<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                             .Select(f => CreateProperty(f, memberSerialization))
                             .ToList();
            
            foreach (var f in fields)
            {
                f.Writable = true;
                f.Readable = true;
            }
            return fields;
        }

        protected override Newtonsoft.Json.Serialization.JsonObjectContract CreateObjectContract(System.Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            contract.DefaultCreator = () => System.Runtime.Serialization.FormatterServices.GetUninitializedObject(objectType);
            return contract;
        }
    }

    public class DatabaseSnapshot
    {
        public Persona[] Personas { get; set; }
        public Habitacion[] Habitaciones { get; set; }
        public Reserva[] Reservas { get; set; }
        public Factura[] Facturas { get; set; }
        public Oferta_carta[] Carta { get; set; }
    }

    public class PersistenceService
    {
        private readonly PersonaService _personaService;
        private readonly HabitacionService _habitacionService;
        private readonly ReservaService _reservaService;
        private readonly FacturaService _facturaService;
        private readonly CartaService _cartaService;
        private readonly string _filePath = "hotel_database.json";
        private readonly JsonSerializerSettings _jsonSettings;

        public PersistenceService(
            PersonaService personaService,
            HabitacionService habitacionService,
            ReservaService reservaService,
            FacturaService facturaService,
            CartaService cartaService)
        {
            _personaService = personaService;
            _habitacionService = habitacionService;
            _reservaService = reservaService;
            _facturaService = facturaService;
            _cartaService = cartaService;

            // Utilizamos TypeNameHandling.Auto para serializar las clases polimórficas
            _jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ContractResolver = new AllFieldsContractResolver()
            };
        }

        public void SaveData()
        {
            var snapshot = new DatabaseSnapshot
            {
                Personas = _personaService.ObtenerTodos(),
                Habitaciones = _habitacionService.ObtenerTodos(),
                Reservas = _reservaService.ObtenerTodas(),
                Facturas = _facturaService.ObtenerTodas(),
                Carta = _cartaService.ObtenerTodos()
            };

            string json = JsonConvert.SerializeObject(snapshot, _jsonSettings);
            File.WriteAllText(_filePath, json);
        }

        public void LoadData(System.IServiceProvider services)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                var snapshot = JsonConvert.DeserializeObject<DatabaseSnapshot>(json, _jsonSettings);

                if (snapshot != null)
                {
                    _personaService.CargarDatos(snapshot.Personas);
                    _habitacionService.CargarDatos(snapshot.Habitaciones);
                    _reservaService.CargarDatos(snapshot.Reservas);
                    _facturaService.CargarDatos(snapshot.Facturas);
                    _cartaService.CargarDatos(snapshot.Carta);
                }
            }
            else
            {
                // Si no existe el archivo, usamos el seeder por defecto y guardamos el estado
                DataSeeder.Seed(services);
                SaveData();
            }
        }
    }
}
