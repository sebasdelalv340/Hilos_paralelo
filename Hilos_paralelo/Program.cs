// See https://aka.ms/new-console-template for more information


using Hilos_paralelo;

FinishEvent finishEvent = new FinishEvent();
Wrapper<Action> finalEvent = new Wrapper<Action>(() => { });

MiHilo t1 = new MiHilo("x",  finalEvent);
MiHilo t2 = new MiHilo("y", finalEvent);

t1.Start();
t2.Start();



