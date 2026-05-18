using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Front_hotel.Services
{
    public class AutoSaveWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _appLifetime;

        public AutoSaveWorker(IServiceProvider serviceProvider, IHostApplicationLifetime appLifetime)
        {
            _serviceProvider = serviceProvider;
            _appLifetime = appLifetime;

            // Al detenerse la aplicación, forzamos un último guardado.
            _appLifetime.ApplicationStopping.Register(OnStopping);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Ejecutamos el guardado automático cada 10 segundos
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);

                // Como el Worker es Singleton, necesitamos crear un Scope para pedir el PersistenceService
                using (var scope = _serviceProvider.CreateScope())
                {
                    var persistence = scope.ServiceProvider.GetRequiredService<PersistenceService>();
                    persistence.SaveData();
                }
            }
        }

        private void OnStopping()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var persistence = scope.ServiceProvider.GetRequiredService<PersistenceService>();
                persistence.SaveData();
            }
        }
    }
}
