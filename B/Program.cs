using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace B {
    class Program {
        static Program() { }

        static void Main( string[] args ) {
            try {
                var ir = Console.In;
                var or = Console.Out;

                //var cl = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
                var cl = CultureInfo.InvariantCulture;
                
                var input = ParseInput( ir );

                int start = 0;
                double approx = 0;
                var win = 0d;
                var sb = new StringBuilder( 20 * input.checks.Length );
                var checks = input.checks;
                for ( int index = 0; index < checks.Length; index++ ) {
                    var check = checks[ index ];
                    var real = Real( input.requests, check, input.T );
                    Approx( input.requests, start, check, input.T, input.c, ref approx );
                    start = check;
                    var error = Math.Abs( approx - real ) / real;

                    sb.Append( real.ToString(cl) );
                    sb.Append( ' ' );
                    sb.Append( approx.ToString( cl ) );
                    sb.Append( ' ' );
                    sb.Append( error.ToString( cl ) );
                    sb.AppendLine();
                }
            }
            catch ( Exception ex ) {

                Console.WriteLine( ex );
            }
            //Console.WriteLine( sb.ToString() );
            Console.ReadLine();
        }

        private static Input ParseInput( TextReader ir ) {
            Input input;
            int T;
            double c;
            int[] requests;
            int[] checks;
            {
                var qs = ir.ReadLine().Split( ' ' ); //.Select( double.Parse ).ToArray();
                T = int.Parse( qs[ 1 ] );
                c = double.Parse( qs[ 2 ] , CultureInfo.InvariantCulture );
            }
            {
                var r = ir.ReadLine().Split( ' ' );
                requests = new int[r.Length];
                for ( int i = 0; i < r.Length; i++ ) requests[ i ] = int.Parse( r[ i ] );
                r = null;
            }
            ir.ReadLine();
            {
                var r = ir.ReadLine().Split( ' ' );
                checks = new int[r.Length];
                for ( int i = 0; i < r.Length; i++ ) checks[ i ] = int.Parse( r[ i ] );
                r = null;
            }
            input = new Input() { checks = checks, requests = requests, c = c, T = T };
            return input;
        }

        private static double Real(int[] requests, int time, int period) {
            long rc = 0;
            for ( int i = Math.Max( 0, time - period ); i < time; i++ ) rc += requests[ i ];
            return (double) rc / period;
        }

        private static void Approx( int[] request, int start, int end, double t, double c, ref double current ) {
            for ( int i = start; i < end; i++ )
                current = ( current + request[ i ] / t ) / c;
        }
    }

    class Input {
        public int T;
        public double c;
        public int[] requests;
        public int[] checks;
    }
}
