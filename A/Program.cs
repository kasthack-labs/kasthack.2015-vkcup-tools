using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace A {
    class Program {
        static void Main( string[] args ) {
            //var sw = new Stopwatch();
            //var input = "100 100\r\n" + String.Join( "\r\n", Enumerable.Repeat( new string( '*', 100 ), 100 ) );

            var matrix = ParseMatrix( Console.In );
            //var matrix = ParseMatrix( new StringReader( input ) );

            //sw.Start();

            //Console.WriteLine( "Parsing complete" );
            //Print( matrix, Console.Out );

            //Console.WriteLine( "Rotating" );
            matrix = rotate( matrix );
            //Print( matrix, Console.Out );

            //Console.WriteLine( "Mirroring" );
            mirror( matrix );
            //Print( matrix, Console.Out );

            //Console.WriteLine( "Scaling" );
            matrix = scale( matrix );
            //Print( matrix, Console.Out );

            //Console.WriteLine( "Result" );
            //Print( matrix, new StreamWriter(Stream.Null) );
            Print( matrix, Console.Out );
            //sw.Stop();
            //Console.Error.WriteLine( sw.Elapsed );
            //Console.ReadLine();
            //Console.WriteLine( "Press enter to exit" );
            //Console.ReadLine();
        }

        private static void Print( bool[][] matrix, TextWriter @out ) {
            for ( int i = 0; i < matrix.Length; i++ ) {
                for ( int j = 0; j < matrix[i].Length; j++ ) {
                    @out.Write( matrix[i][j]?'*' :'.' );
                }
                @out.WriteLine();
            }
            @out.Flush();
        }

        private static bool[][] ParseMatrix( TextReader si ) {
            var sr = si.ReadLine().Split( ' ' ).Select( a => int.Parse( a ) ).ToArray();
            int h = sr[ 1 ], w = sr[ 0 ];

            var matrix = new bool[h][];
            for ( int i = 0; i < matrix.Length; i++ ) matrix[ i ] = new bool[ w ];

            for ( int i = 0; i < h; i++ ) {
                var line = si.ReadLine();
                for ( int j = 0; j < w; j++ ) {
                    matrix[ i][ j ] = line[ j ] == '*';
                }
            }
            //var lines = si.ReadToEnd().Split( new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries );
            //var bits = lines.Select( a => a.Select( b => b == '*' ).ToArray() ).ToArray();
            //var matrix = new bool[sr[ 0 ]][];// sr[ 1 ]];
            //for ( int i = 0; i < matrix.Length; i++ ) matrix[ i ] = new bool[sr[ 1 ]];
            //for ( int i = 0; i < bits.Length; i++ ) {
            //    var bools = bits[ i ];
            //    var length = bools.Length;
            //    for ( int j = 0; j < length; j++ ) matrix[ i ][ j ] = bools[ i ];
            //}
            return matrix;
        }

        private static bool[][] rotate( bool[][] matrix ) {
            var length = matrix[ 0 ].Length;
            var length1 = matrix.Length;

            var tm = new bool[length][];
            for ( int i = 0; i < tm.Length; i++ ) {
                tm[i]= new bool[ length1 ];
            }

            for ( int i = 0; i < matrix.Length; i++ ) {
                for ( int j = 0; j < length; j++ ) {
                    tm[ j ][ i ] = matrix[ matrix.Length - i - 1 ][ j ];
                }
            }
            return tm;
        }
        private static void mirror( bool[][] matrix ) { foreach ( bool[] t in matrix ) Array.Reverse( t ); }

        private static bool[][] scale( bool[][] matrix ) {
            const int S = 2;
            var h = matrix.Length;
            var w = matrix[0].Length;
            var newMatrix = new bool[h * S][];
            for ( int i = 0; i < newMatrix.Length; i++ ) newMatrix[ i ] = new bool[w * S];
            for ( int i = 0; i < h; i++ ) {
                for ( int j = 0; j < w; j++ ) {
                    var val = matrix[ i][j ];
                    writeScale( newMatrix, i, j, val, S );
                }
            }
            return newMatrix;
        }

        private static void writeScale( bool[][] newMatrix, int i, int j, bool val, int s ) {
            var si = i * s;
            var sj = j * s;
            var ei = si + s;
            var ej = sj + s;
            for ( int k = si; k < ei; k++ ) {
                for ( int l = sj; l < ej; l++ ) {
                    newMatrix[ k ][ l ] = val;
                }
            }
        }
    }
}
