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

        static void Main(string[] args)
        {
            InjectTouch();
        }
    }
}
