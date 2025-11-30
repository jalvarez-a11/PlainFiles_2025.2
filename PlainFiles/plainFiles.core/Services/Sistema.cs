using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using plainFiles.core.Models;

namespace plainFiles.core.Services
{
    public static class Sistema
    {
        private const string PersonasFile = "personas.txt";
        public static List<Persona> Personas { get; private set; } = new List<Persona>();

        static Sistema()
        {
            LoadData();
        }

        public static void LoadData()
        {
            Personas.Clear();
            if (!File.Exists(PersonasFile))
            {
                return;
            }
            var lines = File.ReadAllLines(PersonasFile).Where(l => !string.IsNullOrWhiteSpace(l));
            foreach (var line in lines)
            {
                try
                {
                    Personas.Add(Persona.FromFileString(line));
                }
                catch
                {
                    // ignore malformed lines
                }
            }
        }

        public static void SaveChanges()
        {
            var lines = Personas.Select(p => p.ToFileString());
            File.WriteAllLines(PersonasFile, lines);
            LogService.Write("SISTEMA", "Save changes", $"Se guardaron {Personas.Count} personas");
        }

        public static bool IdExists(int id) => Personas.Any(p => p.ID == id);
        public static bool ValidPhone(string phone) => phone.All(char.IsDigit) && phone.Length >= 7;
        public static bool ValidSaldo(decimal saldo) => saldo >= 0;
        public static int NextId() => Personas.Any() ? Personas.Max(p => p.ID) + 1 : 1;
    }
}