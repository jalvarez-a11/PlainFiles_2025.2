
using System;
using System.Linq;
using plainFiles.core.Models;
using plainFiles.core.Services;

namespace PlainFiles.Consola
{
  
    public static class AddPerson
    {
        public static void Run(Sistema sistema, string username) // Recibe la instancia ACTIVA
        {
            

         
            var lista = sistema.GetPersonas();

            Console.WriteLine("=== Agregar Persona ==="); 

           
            int id;
            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("❌ ID inválido (debe ser un número positivo).");
                return;
            }
            if (lista.Any(x => x.ID == id))
            {
                Console.WriteLine("❌ El ID ya existe.");
                return;
            }

            // --- NOMBRES Y APELLIDOS (Punto 3) ---
            Console.Write("Nombres: ");
            string nombres = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(nombres)) { Console.WriteLine("❌ Nombres no pueden estar vacíos."); return; }

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(apellidos)) { Console.WriteLine("❌ Apellidos no pueden estar vacíos."); return; }

            Console.Write("Ciudad: ");
            string ciudad = Console.ReadLine()?.Trim() ?? "";

            // --- TELÉFONO VÁLIDO (Punto 3 - asumo al menos 7 dígitos y solo números) ---
            Console.Write("Teléfono: ");
            string tel = Console.ReadLine()?.Trim() ?? "";
            if (tel.Length < 7)
            {
                Console.WriteLine("❌ Teléfono inválido (mínimo 7 dígitos).");
                return;
            }
            // Puedes agregar validación de solo dígitos aquí si es necesario

            // --- SALDO POSITIVO (Punto 3) ---
            decimal saldo;
            Console.Write("Saldo: ");
            if (!decimal.TryParse(Console.ReadLine(), out saldo) || saldo < 0)
            {
                Console.WriteLine("❌ Saldo inválido (debe ser un número positivo).");
                return;
            }

          
            var nuevaPersona = new Persona
            {
                ID = id,
                Nombres = nombres,
                Apellidos = apellidos,
                Ciudad = ciudad,
                Telefono = tel,
                Saldo = saldo
            };

            sistema.AddPerson(nuevaPersona); 

           
            sistema.SaveChanges(); 
            LogService.Write(username, "Add person", $"Nueva persona agregada con ID {id}."); 

            Console.WriteLine("✅ Persona agregada correctamente.");
        }
    }
}