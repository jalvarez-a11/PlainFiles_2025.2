using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using plainFiles.core.Models;
using plainFiles.core.Services;

namespace PlainFiles.Consola
{
    public static class EditPerson
    {
        public static void Run(string username)
        {
            var sistema = new Sistema();
            var lista = sistema.GetAll();

            Console.Write("ID a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var p = lista.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                Console.WriteLine("No existe.");
                return;
            }

            Console.Write($"Nombres ({p.Nombres}): ");
            string n = Console.ReadLine();
            if (!string.IsNullOrEmpty(n)) p.Nombres = n;

            Console.Write($"Apellidos ({p.Apellidos}): ");
            string a = Console.ReadLine();
            if (!string.IsNullOrEmpty(a)) p.Apellidos = a;

            Console.Write($"Ciudad ({p.Ciudad}): ");
            string c = Console.ReadLine();
            if (!string.IsNullOrEmpty(c)) p.Ciudad = c;

            Console.Write($"Teléfono ({p.Telefono}): ");
            string t = Console.ReadLine();
            if (!string.IsNullOrEmpty(t)) p.Telefono = t;

            Console.Write($"Balance ({p.Balance}): ");
            string b = Console.ReadLine();
            if (!string.IsNullOrEmpty(b) && decimal.TryParse(b, out decimal saldo))
                p.Balance = saldo;

            sistema.SaveAll(lista);
            LogService.Write(username, "Editar", $"Editó persona {id}");

            Console.WriteLine("Registro actualizado.");
            Console.ReadKey();
        }
    }
}
