using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Hilos_hash;

public class MiHilo
{
    private readonly string _tag;
    private readonly Thread _hilo;
    private readonly string _hash;
    private readonly string[] _passwords;
    private readonly Wrapper<Action> _finalizar;
    private readonly Stopwatch _stopwatch;

    public MiHilo(string tag, string hash, string[] passwords, Wrapper<Action> finalizar, Stopwatch stopwatch)
    {
        _tag = tag;
        _hash = hash;
        _passwords = passwords;
        _finalizar = finalizar;
        _stopwatch = stopwatch;
        _hilo = new Thread(_getHash);
    }

    public void Start()
    {
        _stopwatch.Start();
        _hilo.Start();
    }

    void _getHash()
    {
        foreach (string password in _passwords)
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
                if (hashString.ToString() == _hash)
                {
                    _finalizar.Value += () => { Console.WriteLine($"Hilo {_tag}: {password}"); };
                    _finalizar.Value.Invoke();
                    _hilo.Interrupt();
                    _stopwatch.Stop();
                }
            }
        }
        _hilo.Interrupt();
        _stopwatch.Stop(); 
    }
}