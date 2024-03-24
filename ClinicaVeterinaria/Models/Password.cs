using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ClinicaVeterinaria.Models
{
    public class Password
    {
        
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Calcola l'hash della password
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Converti l'array di byte in una stringa esadecimale
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

            public static bool VerifyPassword(string password, string hashedPassword)
            {
                // Verifica se la password fornita corrisponde all'hash memorizzato
                string hashedInput = HashPassword(password);
                return string.Equals(hashedInput, hashedPassword);
            }
        
    }
}