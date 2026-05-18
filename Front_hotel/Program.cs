using Front_hotel.Components;
using Front_hotel.Services;
using biblioteca_hotel.Eventos;
using biblioteca_hotel.Interfaces;
using biblioteca_hotel.Modelos.Core;
using biblioteca_hotel.Modelos.Gestión;
using biblioteca_hotel.Servicios;
using biblioteca_hotel.Utilidades;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 4000;
    config.SnackbarConfiguration.HideTransitionDuration = 200;
    config.SnackbarConfiguration.ShowTransitionDuration = 200;
});
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<IAuditLogger, FileAuditLogger>();
builder.Services.AddSingleton<HotelStateService>();
builder.Services.AddSingleton<UiObservador>();

builder.Services.AddScoped<biblioteca_hotel.Aspectos.Validador>();
builder.Services.AddSingleton<ReservaService>();
builder.Services.AddSingleton<FacturaService>();
builder.Services.AddSingleton<PersonaService>();
builder.Services.AddSingleton<ProductoService>();
builder.Services.AddSingleton<HabitacionService>();
builder.Services.AddSingleton<CartaService>();
builder.Services.AddSingleton<Oficina>();
builder.Services.AddSingleton<biblioteca_hotel.Modelos.Gestión.Recepcion>();
builder.Services.AddSingleton<biblioteca_hotel.Modelos.Servicios.Restaurante>();
builder.Services.AddSingleton<biblioteca_hotel.Modelos.Servicios.Lavandera>();
builder.Services.AddTransient<LectorArchivos>(_ => new LectorArchivos(string.Empty));
builder.Services.AddSingleton<IGestionHotel>(_ => new HotelProxy(new Oficina()));
builder.Services.AddSingleton<GestorNotificaciones>(provider =>
{
    var gestor = new GestorNotificaciones();
    var observador = provider.GetRequiredService<UiObservador>();
    gestor.suscribir(observador);
    EventoBase.SetGestor(gestor);
    return gestor;
});

// Registro de persistencia y autoguardado local
builder.Services.AddSingleton<PersistenceService>();
builder.Services.AddHostedService<AutoSaveWorker>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var persistence = scope.ServiceProvider.GetRequiredService<PersistenceService>();
    persistence.LoadData(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
