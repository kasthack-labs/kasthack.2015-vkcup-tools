using System;
using System.Globalization;

namespace B {
    class Program {
        static void Main( string[] args ) {
            int T;
            double c;
            int[] requests;
            int[] checks;

            var ir = Console.In;
            var or = Console.Out;
            var cult = CultureInfo.InvariantCulture;
            {
                var qs = ir.ReadLine().Split( ' ' );//.Select( double.Parse ).ToArray();
                T = int.Parse( qs[1] );
                c = double.Parse(qs[ 2 ],cult);
            }
            {
                var r = ir.ReadLine().Split( ' ' );
                requests = new int[r.Length];
                for ( int i = 0; i < r.Length; i++ ) {
                    requests[ i ] = int.Parse( r[ i ] );
                }
                r = null;
            }
            ir.ReadLine();
            {
                var r = ir.ReadLine().Split( ' ' );
                checks = new int[r.Length];
                for ( int i = 0; i < r.Length; i++ ) {
                    checks[ i ] = int.Parse( r[ i ] );
                }
                r = null;
            }
            int start = 0;
            decimal approx = 0;
            foreach ( var check in checks ) {
                var real = Real( requests, check, T );
                Approx( requests, start, check, T, c, ref approx );
                start = check;
                var error = Math.Abs( approx - (decimal) real ) / (decimal) real;
                or.WriteLine( String.Format( cult, "{0:F6} {1:F6} {2:F6}", real, approx, error ));
            }
        }

        private static double Real(int[] requests, int time, int period) {
            long rc = 0;
            for ( int i = Math.Max( 0, time - period ); i < time; i++ ) rc += requests[ i ];
            return (double) rc / period;
        }

        private static void Approx( int[] request, int start, int end, double t, double c, ref decimal current ) {
            for ( int i = start; i < end; i++ )
                current = ( current + request[ i ] / (decimal) t ) / (decimal) c;
        }
    }
}
