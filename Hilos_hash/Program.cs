// See https://aka.ms/new-console-template for more information

using Hilos_hash;

string password = "!!!!!!1";

int hash = password.GetHashCode();

string[] passwords = { File.ReadAllText("C:\\Users\\Sebas\\RiderProjects\\Hilos_paralelo\\Hilos_hash\\passwords.txt") };

Wrapper<Action> finalEvent = new Wrapper<Action>(() => { });

MiHilo t1 = new MiHilo(hash, passwords, finalEvent);

t1.Start();
