// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Hilos_hash;

string[] passwords = File.ReadAllLines("C:\\Users\\Sebas\\RiderProjects\\Hilos_paralelo\\Hilos_hash\\2151220-passwords.txt");

string password = "! love you";

static string HashPassword(string password)
{
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
        return hashString.ToString();
    }
}

string hashedPassword = HashPassword(password);

static bool BuscarConHilos(string[] passwords, string targetHash, int numHilos)
{
    int totalPasswords = passwords.Length;
    int tramo = totalPasswords / numHilos; // Tamaño de cada "trozo" para los hilos
    bool encontrado = false;
    object lockObject = new object();
    List<MiHilo> hilos = new List<MiHilo>();
    
    for (int i = 0; i < numHilos; i++)
    {
        // Calcular el índice de inicio y fin de cada segmento
        int start = i * tramo;
        int end = (i == numHilos - 1) ? totalPasswords : (start + tramo); // El último hilo toma el resto

        string[] segmento = passwords.Skip(start).Take(end - start).ToArray();

        // Crear hilo pasando el segmento de contraseñas
        MiHilo hilo = new MiHilo($"{i}", targetHash, segmento, new Wrapper<Action>(() => { }), new Stopwatch());

        hilos.Add(hilo);
        hilo.Start();
    }
    
    // Esperar a que todos los hilos terminen
    foreach (var hilo in hilos)
    {
        hilo.Join();
    }

    // Verificar si la contraseña fue encontrada
    lock (lockObject)
    {
        encontrado = MiHilo.Found;
    }

    return encontrado;
}


Console.WriteLine($"\n--- Probando con {16} hilos ---");

Stopwatch sw = Stopwatch.StartNew();
bool encontrado = BuscarConHilos(passwords, hashedPassword, 16); // Si nuestra función devuelve true, los hilos se detendrán gracias a nuestra variable compartida.
sw.Stop();

Console.WriteLine($"Tiempo de ejecución con {16} hilos: {sw.ElapsedMilliseconds} ms");
if (encontrado)
{
    Console.WriteLine("Contraseña encontrada!");
}




