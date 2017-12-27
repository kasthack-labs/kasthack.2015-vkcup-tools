using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//слава сатане
namespace A {
    class Program {
        static void Main( string[] args ) {
            TextReader input;
            TextWriter output;
            input = Console.In;
            output = Console.Out;
            Main2( input, output );
        }

        private static void Main2( TextReader input, TextWriter output ) {
            
            var separator = new char[] { ' ' };
            var l = input.ReadLine().Split( separator,2 );
            var pairsCount = int.Parse( l[ 0 ] );
            var mp = double.Parse( l[ 1 ] );

            var pairs = new Tuple<int, int>[ pairsCount ];

            for ( int i = 0; i < pairsCount; i++ ) {
                var l2 = input.ReadLine().Split( separator,2 );
                pairs[i] = new Tuple<int, int>( int.Parse( l2[0] ), int.Parse( l2[1] ) );
            }
            var uf = pairs.SelectMany( a => new[] { a.Item1, a.Item2 } ).Distinct().OrderBy( a => a ).ToArray();
            var usersFriends = new Dictionary<int, HashSet<int>>();
            //симметрия пар
            foreach ( var p in pairs ) {
                HashSet<int> tg;
                if (!usersFriends.TryGetValue( p.Item1, out tg ) ) {
                    tg = new HashSet<int> { p.Item2 };
                    usersFriends.Add( p.Item1, tg );
                }
                else {
                    tg.Add( p.Item2 );
                }
                if ( !usersFriends.TryGetValue( p.Item2, out tg ) ) {
                    tg = new HashSet<int> { p.Item1 };
                    usersFriends.Add( p.Item2, tg );
                }
                else {
                    tg.Add( p.Item1 );
                }
            }
            
            var ls = new Dictionary<int, SortedList<int, int>>(usersFriends.Count);

            foreach ( var p in usersFriends ) {
                var targetFriends = p.Value;
                var sl = new SortedList<int,int>();
                foreach ( var fp2 in usersFriends ) {
                    var relId = fp2.Key;
                    if (relId == p.Key || targetFriends.Contains( relId )) continue;

                    var relatedFriends = fp2.Value;
                    var mutualFriends = 0;
                    var sc = targetFriends.Count > relatedFriends.Count ? relatedFriends : targetFriends;
                    var rc = targetFriends.Count <= relatedFriends.Count ? relatedFriends : targetFriends;
                    foreach ( var v in sc ) if ( rc.Contains( v ) ) mutualFriends++;
                    if ( (double)mutualFriends >= targetFriends.Count * mp / 100 )
                        sl.Add( relId, relId );
                }
                ls.Add( p.Key, sl );
            }

            foreach ( var user in uf ) {
                SortedList<int,int> l1;
                if ( !ls.TryGetValue( user, out l1 ) ) {
                    output.WriteLine( "{0}: 0", user );
                }
                else {
                    var value = l1;
                    var vc = value.Count;
                        output.WriteLine( "{0}: {2} {1}", user, String.Join( " ", value.Values ), vc );
                }
            }
        }
    }
}
