using System;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace Front_hotel.Services
{
    public class FileAuditLogger : IAuditLogger
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public FileAuditLogger(IHostEnvironment env)
        {
            var folder = Path.Combine(env.ContentRootPath, "App_Data", "registros");
            Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, "auditoria.log");
        }

        public void Log(string message)
        {
            var linea = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            lock (_lock)
            {
                File.AppendAllLines(_filePath, new[] { linea });
            }
        }
    }
}
