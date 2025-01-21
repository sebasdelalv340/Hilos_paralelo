namespace Hilos_hash;

public class MiHilo
{
    private readonly Thread _hilo;
    private readonly byte[] _hash;
    private readonly string[] _passwords;
    private readonly Wrapper<Action> _finalizar;

    public MiHilo(byte[] hash, string[] passwords, Wrapper<Action> finalizar)
    {
        _hash = hash;
        _passwords = passwords;
        _finalizar = finalizar;
        _hilo = new Thread(_getHash);
    }

    public void Start()
    {
        _hilo.Start();
    }

    void _getHash()
    {
        foreach (var password in _passwords)
        {
            byte[] hashNuevo = password;
            if (hashNuevo == _hash)
            {
                _finalizar.Value += () => { Console.WriteLine($"Esta es la contraseña: {password}");};
                _finalizar.Value.Invoke();
                _hilo.Interrupt();
            }
        }
    }
}