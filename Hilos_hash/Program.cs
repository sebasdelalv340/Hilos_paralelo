// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Hilos_hash;

string[] passwords = File.ReadAllLines("C:\\Users\\Sebas\\RiderProjects\\Hilos_paralelo\\Hilos_hash\\2151220-passwords.txt");

//string password = "!7rus7n01*";
Random random = new Random();
string password = passwords[random.Next(passwords.Length)];

int mitad = passwords.Length / 2;
string[] primeraMitad = passwords.Take(mitad).ToArray();
string[] segundaMitad = passwords.Skip(mitad).ToArray();

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

// Token de cancelación
CancellationTokenSource cts = new CancellationTokenSource();
CancellationToken token = cts.Token;

// Variables para almacenar tiempos
Stopwatch sw1 = new Stopwatch();
Stopwatch sw2 = new Stopwatch();

MiHilo t1 = new MiHilo("A",password, primeraMitad, finalEvent, sw1);
MiHilo t2 = new MiHilo("B", password, segundaMitad, finalEvent, sw2);

t1.Start();
t2.Start();

Console.WriteLine($"Tiempo de ejecución del hilo A: {sw1.ElapsedMilliseconds} ms");
Console.WriteLine($"Tiempo de ejecución del hilo B: {sw2.ElapsedMilliseconds} ms");



