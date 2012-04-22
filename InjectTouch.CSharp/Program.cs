using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InjectTouch.CSharp
{
    class Program
    {
        [DllImport("InjectTouch.dll")]
        public static extern void InjectTouch();

        [DllImport("InjectTouch.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Drag(int x, int y);


        static void Main(string[] args)
        {
            Console.WriteLine("Enter to start");
            Console.ReadLine();

            Drag(300, 300);
        }
    }
}
