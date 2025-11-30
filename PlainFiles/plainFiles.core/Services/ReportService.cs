using System;
using System.Linq;
using plainFiles.core.Models;

namespace plainFiles.core.Services
{
    public class ReportService
    {
        public void GenerateReport()
        {
            var data = Sistema.Personas
                        .GroupBy(p => p.Ciudad)
                        .OrderBy(g => g.Key);

            decimal totalGeneral = 0;

            foreach (var group in data)
            {
                Console.WriteLine($"\nCiudad: {group.Key}");
                Console.WriteLine("ID\tNombres\tApellidos\tSaldo");
                Console.WriteLine("----------------------------------------------");

                decimal subtotal = 0;

                foreach (var p in group.OrderBy(x => x.ID))
                {
                    Console.WriteLine($"{p.ID}\t{p.Nombres}\t{p.Apellidos}\t{p.Saldo,10:N2}");
                    subtotal += p.Saldo;
                }

                totalGeneral += subtotal;

                Console.WriteLine("\t\t\t=======");
                Console.WriteLine($"Total: {group.Key}\t{subtotal:N2}");
            }

            Console.WriteLine("\n\t\t\t=======");
            Console.WriteLine($"Total General:\t{totalGeneral:N2}");
        }
    }
}