using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plainFiles.core.Models
{
    public class User
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool Active { get; set; } = true;
        public int FailedAttempts { get; set; } = 0;

        public string ToFileString()
        {
            // Save Active in lower-case true/false
            return $"{Username},{Password},{Active.ToString().ToLower()}";
        }

        public static User FromFileString(string line)
        {
            var parts = line.Split(',');
            return new User
            {
                Username = parts[0],
                Password = parts[1],
                Active = bool.Parse(parts[2])
            };
        }
    }
}

