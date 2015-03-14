using System;

namespace C {
    class Program {
        static void Main() {
            var s = Console.In.ReadLine().ToCharArray();
            var t = Console.In.ReadLine().ToCharArray();
            var res = Find( t, s );
            Console.Out.WriteLine(res);
        }

        private static int Find( char[] t, char[] s ) {
            var l = FindShortest( t, s );
            if ( l < 0 ) return 0;
            Array.Reverse( t );
            Array.Reverse( s );
            var r = FindShortest( t, s );
            if ( r + l > t.Length ) return 0;
            return t.Length - r - l + 1;
        }
        private static int FindShortest( char[] tr, char[] sr ) {
            int cur = 0;
            int i = 0;
            for ( ; i < tr.Length; i++ ) {
                if ( tr[ i ] != sr[ cur ] ) continue;
                cur++;
                if ( cur >= sr.Length ) break;
            }
            if ( cur < sr.Length ) return -1;
            return i + 1;
        }
    }
}
