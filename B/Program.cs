using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B {
    class Program {
        static void Main( string[] args ) {
            var qs = Console.In.ReadLine().Split( ' ' ).Select( double.Parse ).ToArray();

            //var n = (int) qs[ 0 ];
            var T = (int) qs[ 1 ];
            var c = qs[ 2 ];
            //var requests = new int[n];
            //for ( int i = 0; i < n; i++ ) requests[ i ] = int.Parse( Console.In.ReadLine() );
            var requests = Console.ReadLine().Split( ' ' ).Select( int.Parse ).ToArray();
            //var m = int.Parse( Console.In.ReadLine() );
            Console.ReadLine();
            //var checks = new int[m];
            //for ( int i = 0; i < m; i++ ) checks[ i ] = int.Parse( Console.In.ReadLine() );
            var checks = Console.ReadLine().Split( ' ' ).Select( int.Parse ).ToArray();

            int start = 0;
            decimal approx = 0;

            foreach ( var check in checks ) {
                var real = Real( requests, check, T );

                Approx( requests, start, check, T, c, ref approx );
                start = check + 1;

                var error = Math.Abs( approx - (decimal) real ) / (decimal) real;

                Console.Out.WriteLine( "{0:F5} {1:F5} {2:F5}", real, approx, error );
            }
            Console.ReadLine();
        }

        private static double Real(int[] requests, int time, int period) {
            long rc = 0;
            for ( int i = time-period; i < time; i++ ) {
                rc += requests[ i ];
            }
            return (double) rc / time;
        }

        private static void Approx( int[] request, int start, int end, double t, double c, ref decimal current ) {
            for ( int i = start; i < end; i++ ) current = ( current + request[ i ] / (decimal) t ) / (decimal) c;
        }
    }
}
