using System;
using System.Collections.Generic;
using System.Linq;

namespace D {
    class Program {
        static void Main( string[] args ) {
            var sr = Console.In.ReadLine().Split( ' ' ).Select( int.Parse ).ToArray();
            var vids = sr[ 0 ];
            var srvs = sr[ 1 ];
            var movs = ParseUploads( vids );
            for ( int i = 0; i < movs.Count; i++ ) {
                
            }
        }
        private static List<Upload> ParseUploads( int vids ) {
            var movs = new List<Upload>( vids );
            for ( int i = 0; i < vids; i++ ) {
                var r = Console.In.ReadLine().Split( ' ' );
                movs.Add(
                    new Upload {
                        Index = i,
                        Date = int.Parse( r[ 0 ] ),
                        Length = int.Parse( r[ 1 ] ),
                        Complete = -1
                    } );
            }
            return movs;
        }
    }

    internal class ServerPool {
        private readonly int _count;
        private int _freeCount;
        private Queue<Upload> _queue;
        public ServerPool( int count ) {
            _count = count;
            _queue = new Queue<Upload>();
        }
        public void Add(Upload u) {
            
        }
    }

    class Upload {
        public int Index { get; set; }
        public int Date { get; set; }
        public int Length { get; set; }
        public int Complete { get; set; }
    }
}
