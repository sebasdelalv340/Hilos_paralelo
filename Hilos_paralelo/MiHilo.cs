namespace Hilos_paralelo;

public class MiHilo
{
    private readonly Thread _hilo;
    private readonly String _texto;
    private readonly Wrapper<Action> _finalizar;

    public MiHilo(String texto, Wrapper<Action> finalizar)
    {
        _texto = texto;
        _finalizar = finalizar;
        _finalizar.Value += () => { Console.Write($"Hilo {texto}"); };
        _hilo = new Thread(_process);
    }

    public void Start()
    {
        _hilo.Start();
    }

    void _process()
    {
        for (int i = 0; i < 1000; i++) { Console.Write(_texto); }
        _finalizar.Value.Invoke();
        _hilo.Interrupt();
    }
}