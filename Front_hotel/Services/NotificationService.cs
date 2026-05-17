using System;
using System.Collections.Generic;
using biblioteca_hotel.Enums;

namespace Front_hotel.Services
{
    public class NotificationEntry
    {
        public string Message { get; init; } = string.Empty;
        public string Tone { get; init; } = "tone-blue";
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }

    public class NotificationService
    {
        private readonly List<NotificationEntry> _items = new();
        public event Action? OnChanged;

        public NotificationEntry[] GetAll() => _items.ToArray();

        public void Add(string message, string tone)
        {
            _items.Insert(0, new NotificationEntry { Message = message, Tone = tone, Timestamp = DateTime.Now });
            OnChanged?.Invoke();
        }

        public void AddFromEvent(TipoEvento evento)
        {
            var (message, tone) = evento switch
            {
                TipoEvento.ConsumoMinibar => ("Consumo de minibar registrado.", "tone-mint"),
                TipoEvento.CancelacionReserva => ("Reserva cancelada.", "tone-rose"),
                TipoEvento.CheckInCompletado => ("Check-in completado.", "tone-blue"),
                TipoEvento.FacturaGenerada => ("Factura generada.", "tone-lilac"),
                _ => ("Evento registrado.", "tone-yellow")
            };

            Add(message, tone);
        }
    }
}
