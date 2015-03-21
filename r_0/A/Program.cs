using System;
using System.IO;
using System.Linq;

namespace A {
    class Program {
        static void Main( string[] args ) {
            var matrix = ParseMatrix( Console.In );

            matrix = rotate( matrix );//можно заменить эти две функции на ротейт по диагонали, но не важно
            mirror( matrix );

            matrix = scale( matrix );
            Print( matrix, Console.Out );
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
