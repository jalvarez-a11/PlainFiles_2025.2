using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using plainFiles.core.Services;

namespace PlainFiles.Consola
{
    public static class DeletePerson
    {
        public static void Run(string username)
        {
            var sistema = new Sistema();
            var lista = sistema.GetAll();

            Console.Write("ID a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var p = lista.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                Console.WriteLine("No existe.");
                return;
            }

            Console.WriteLine($"Eliminar {p.Nombres} {p.Apellidos} (S/N)?");
            if (Console.ReadLine()?.ToUpper() != "S") return;

            lista.Remove(p);
            sistema.SaveAll(lista);

            LogService.Write(username, "Eliminar", $"Eliminó persona {id}");

            Console.WriteLine("Eliminado.");
            Console.ReadKey();
        }
    }
}
