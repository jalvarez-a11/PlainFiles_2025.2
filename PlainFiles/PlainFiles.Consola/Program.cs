// Namespace: PlainFiles.Consola
using System;
using plainFiles.core.Models;
using plainFiles.core.Services;

namespace PlainFiles.Consola
{
    class Program
    {
        static void Main()
        {
            var auth = new AuthService();
            Console.WriteLine("=== 💾 PlainFiles - Consola ===\n");

            User? logged = null;

            // Bucle de Autenticación
            while (logged == null)
            {
                Console.Write("Usuario: ");
                string user = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(user)) return;

                Console.Write("Contraseña: ");
                string pass = ReadPassword(); // Usando el helper
                Console.WriteLine();

                if (auth.ValidateCredentials(user, pass, out User? u))
                {
                    logged = u;
                    auth.ResetAttempts(user);
                    LogService.Write(user, "Login", "Inicio exitoso");
                    Console.WriteLine($"\n✅ ¡Bienvenido, {user}!");
                    break;
                }
                else
                {
                    string message = auth.RegisterFailedAttempt(user);
                    Console.WriteLine($"\n❌ {message}\n");
                }
            }

            if (logged != null)
            {
              
                var sistema = new Sistema();

              
                MenuPrincipal.ShowMenu(sistema, logged.Username);
            }
            else
            {
                Console.WriteLine("Saliendo del programa por fallos de autenticación.");
            }
        }

        static string ReadPassword()
        {
            
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return password;
        }
    }
}
