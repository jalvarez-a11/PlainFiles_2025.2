using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using plainFiles.core.Services;

namespace PlainFiles.Consola
{
    public static class ListPersons
    {
        public static void Run(string username)
        {
            var sistema = new Sistema();
            var lista = sistema.GetAll();

            Console.WriteLine("ID\tNombres\tApellidos\tCiudad\tSaldo");

            foreach (var p in lista)
            {
                Console.WriteLine($"{p.Id}\t{p.Nombres}\t{p.Apellidos}\t{p.Ciudad}\t{p.Balance:N2}");
            }

            LogService.Write(username, "Listar", "Listado de personas");

            Console.ReadKey();
        }
    }
}
