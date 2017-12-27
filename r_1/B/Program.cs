using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B {
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
            //Console.WriteLine( sw.Elapsed );
            Console.ReadLine();
#endif
        }

        private static void Main2( TextReader input, TextWriter output ) {
            var sr = input.ReadLine().Split( ' ' ).Select( int.Parse ).ToArray();
            var n = sr[ 0 ];
            var m = sr[ 1 ];
            var k = sr[ 2 ];
            var q = sr[ 3 ];

            Tuple<int,int>[] ks = new Tuple<int, int>[k];

            for ( int i = 0; i < k; i++ ) {
                var l = input.ReadLine().Split( ' ' );
                ks[i] = new Tuple<int, int>( int.Parse( l[0] ), int.Parse( l[1] ) );
            }

            Tuple<int,int,int, int>[] fs = new Tuple<int, int, int, int>[q];

            for ( int i = 0; i < q; i++ ) {
                var l = input.ReadLine().Split( ' ' );
                fs[ i ] = new Tuple<int, int,int,int>(
                    int.Parse( l[ 0 ] ),
                    int.Parse( l[ 1 ] ),
                    int.Parse( l[ 2 ] ),
                    int.Parse( l[ 3 ] )
                );
            }


            //sort
            Comparison<Tuple<int, int>> sc = ( a, b ) => {
                var _ = a.Item1.CompareTo( b.Item1 );
                if ( _ > 0 ) return _;
                return a.Item2.CompareTo( b.Item2 );
            };
            Comparison<Tuple<int, int>> ac = ( a, b ) => a.Item1.CompareTo( b.Item1 );
            Array.Sort( ks,
                        sc );
            //quickchecks
            var _xr = new HashSet<int>( ks.Select( a => a.Item1 ) );
            var _yr = new HashSet<int>( ks.Select( a => a.Item2 ) );

            foreach ( var tuple in fs ) {
                var xt = new Boolean[ tuple.Item3 - tuple.Item1 + 1];
                var yt = new Boolean[ tuple.Item4 - tuple.Item2 + 1];
                bool qc = false;
                    var first = BinarySearch( ks, new Tuple<int, int>( tuple.Item1, 0 ), ac, 0, ks.Length );
                    var last = BinarySearch( ks, new Tuple<int, int>( tuple.Item3, 0 ), ac, 0, ks.Length );
                    if ( first < 0
                         || last < 0 ) {
                        qc = false;
                    }
                    else {
                        var item3 = tuple.Item3 - 1;
                        for ( int i = first; i <= last; i++ ) {
                            var cur = ks[ i ];
                            xt[ cur.Item1 - tuple.Item1 ] = true;
                            if ( cur.Item2 >= tuple.Item2
                                 && cur.Item2 <= tuple.Item4 ) yt[ cur.Item2 - tuple.Item2 ] = true;
                        }
                    }
                if ( !qc && ( xt.All( a=>a ) && yt.All( a => a ) ) ) {
                    output.WriteLine("YES");
                }
                else {
                    output.WriteLine("NO");
                }
            }
        }
        public static int BinarySearch<T>( T[] array, T value, Comparison<T> comparison, int min, int max ) {
            if ( array.Length == 0 )
                return -1;
            int iMin = min,
                    iMax = min + max - 1,
                    iCmp = 0;
            int iMid;
            while ( iMin <= iMax ) {
                iMid = iMin + ( ( iMax - iMin ) / 2 );
                iCmp = comparison( array[ iMid ], value );
                if ( iCmp == 0 ) return iMid;
                if ( iCmp > 0 ) iMax = iMid - 1;
                else iMin = iMid + 1;
            }
            return ~iMin;
        }
    }
}
