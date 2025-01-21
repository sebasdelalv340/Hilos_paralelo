// See https://aka.ms/new-console-template for more information

using System.Text;
using Hilos_hash;

string password = "!!!!!!1";
Encoding unicode = Encoding.Unicode;

byte[] unicodeBytes = unicode.GetBytes(password);

string[] passwords = { File.ReadAllText("C:\\Users\\Sebas\\RiderProjects\\Hilos_paralelo\\Hilos_hash\\passwords.txt") };

Wrapper<Action> finalEvent = new Wrapper<Action>(() => { });

MiHilo t1 = new MiHilo(unicodeBytes, passwords, finalEvent);

t1.Start();
