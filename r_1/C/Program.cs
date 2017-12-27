using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C {
    class Program {
        static void Main( string[] args ) {
            TextReader input;
            TextWriter output;
#if DEBUG
            var sw = new Stopwatch();
            sw.Start();
#endif
            input = Console.In;
            output = Console.Out;
            Main2( input, output );
#if DEBUG
            sw.Stop();
            Console.WriteLine( sw.Elapsed );
#endif
        }

        private static void Main2( TextReader input, TextWriter output ) {

        }
    }
}
