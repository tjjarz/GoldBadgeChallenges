using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeAppChallenge
{
    public interface IConsole 
    {
        string ReadLine();
        
        void Write(string str);
        void WriteLine(string str);
        void WriteLine(object obj);
        void Clear();
        ConsoleKeyInfo ReadKey(); //consolekeyinfo = what is it?
        void Title(string str);

        //void ForegroundColor(ConsoleColor color);
        //ConsoleColor ForegroundColor { get; set; }

        //ConsoleColor BackgroundColor { get; set; }

    }

    public class MyConsole : IConsole
    {
        //public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public ConsoleColor ForegroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public ConsoleColor BackgroundColor { get { return BackgroundColor; } set(ConsoleColor color) { Console.BackgroundColor = BackgroundColor; } }

        public void Clear()
        {
            Console.Clear();
        }

        public void Title(string str)
        {
            Console.Title = str;
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string str)
        {
            Console.Write(str);
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        public void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}