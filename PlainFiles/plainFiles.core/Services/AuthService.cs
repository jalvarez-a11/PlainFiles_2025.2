
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using plainFiles.core.Models;

namespace plainFiles.core.Services
{
    public class AuthService
    {
        private const string UsersFile = "Users.txt";

        public List<User> LoadUsers()
        {
            if (!File.Exists(UsersFile))
                return new List<User>();

            return File.ReadAllLines(UsersFile)
                       .Where(line => !string.IsNullOrWhiteSpace(line))
                       .Select(User.FromFileString)
                       .ToList();
        }

        public void SaveUsers(List<User> users)
        {
            var lines = users.Select(u => u.ToFileString());
            File.WriteAllLines(UsersFile, lines);
        }

        public User? FindByUsername(string username)
        {
            return LoadUsers().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool ValidateCredentials(string username, string password, out User? user)
        {
            user = FindByUsername(username);
            if (user == null) return false;
            if (!user.Active) return false;
            return user.Password == password;
        }

        public void RegisterFailedAttempt(string username)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null) return;
            user.FailedAttempts++;
            if (user.FailedAttempts >= 3)
            {
                user.Active = false;
                LogService.Write(username, "Bloqueo de usuario", "Usuario bloqueado por 3 intentos fallidos");
            }
            SaveUsers(users);
        }

        public void ResetAttempts(string username)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null) return;
            user.FailedAttempts = 0;
            SaveUsers(users);
        }
    }
}
