using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;

namespace plainFiles.core.Services
{
    public static class LogService
    {
        private const string LogFile = "log.txt";

        public static void Write(string username, string action, string details = "")
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Usuario: {username ?? "SISTEMA"} | Operación: {action} | Detalles: {details}";
            try
            {
                File.AppendAllText(LogFile, line + Environment.NewLine);
            }
            catch
            {
              
            }
        }
    }
}

