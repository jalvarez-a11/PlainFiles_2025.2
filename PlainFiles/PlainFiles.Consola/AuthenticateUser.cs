using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static bool AuthenticateUser(Sistema sistema, string username)
{
    Console.WriteLine("=== Autenticación ===");

    for (int attempts = 3; attempts > 0; attempts--)
    {
        Console.Write("Usuario: ");
        string username = Console.ReadLine();
        Console.Write("Contraseña: ");
        string password = Console.ReadLine();

        var user = usuarios.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            Console.WriteLine($"Usuario no encontrado. Intentos restantes: {attempts - 1}");
        }
        else if (!user.IsActive)
        {
            Console.WriteLine($"El usuario {username} está bloqueado.");
            return false;
        }
        else if (user.Password == password)
        {
            CurrentUser = username;
            LogOperation("Login", $"Inicio de sesión exitoso para {username}.");
            return true; // Acceso concedido
        }
        else
        {
            Console.WriteLine($"Contraseña incorrecta. Intentos restantes: {attempts - 1}");
        }
    }

    // Bloqueo del usuario si falló los 3 intentos
    Console.WriteLine("⛔ Ha agotado sus intentos. Bloqueando la cuenta...");

    var lastAttemptedUser = usuarios.FirstOrDefault(u => u.Username.Equals(Console.ReadLine(), StringComparison.OrdinalIgnoreCase));
    if (lastAttemptedUser != null)
    {
        lastAttemptedUser.IsActive = false;
        SaveChanges(); // Guardar el estado de bloqueo inmediatamente
        LogOperation("Login Fail & Lock", $"El usuario {lastAttemptedUser.Username} ha sido bloqueado.");
    }

    return false; // Acceso denegado
}