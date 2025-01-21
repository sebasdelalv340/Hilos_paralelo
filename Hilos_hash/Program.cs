// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;
using Hilos_hash;

string[] passwords = File.ReadAllLines("C:\\Users\\Sebas\\RiderProjects\\Hilos_paralelo\\Hilos_hash\\2151220-passwords.txt");

//string password = "!7rus7n01*";
Random random = new Random();
string password = passwords[random.Next(passwords.Length)];

using (SHA256 sha256 = SHA256.Create())
{
    byte[] bytes = Encoding.UTF8.GetBytes(password);
    byte[] hashBytes = sha256.ComputeHash(bytes);

    // Convertir el hash a una cadena hexadecimal
    StringBuilder hashString = new StringBuilder();
    foreach (byte b in hashBytes)
    {
        hashString.Append(b.ToString("x2"));
    }
    password = hashString.ToString();
}



Wrapper<Action> finalEvent = new Wrapper<Action>(() => { });

MiHilo t1 = new MiHilo(password, passwords, finalEvent);

t1.Start();
