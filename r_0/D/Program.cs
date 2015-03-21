using System;
using System.Text;

namespace D {
    class Program {
        static void Main() {
            var input = ParseStdIn();
            Impl2(input);
            Print( input );
        }

        private static void Print( Input input ) {
            var sb = new StringBuilder();
            var uploads = input.uploads;
            for ( int index = 0; index < uploads.Length; index++ )
                sb.AppendLine( uploads[ index ].Complete.ToString() );
            Console.Out.Write(sb.ToString());
        }

        private static Input ParseStdIn() {
            var sr = Console.In.ReadLine().Split( ' ' );
            var vids = int.Parse(sr[ 0 ]);
            var srvs = int.Parse(sr[ 1 ]);
            var movs = ParseUploads( vids );
            var input = new Input { Servers = srvs, uploads = movs };
            return input;
        }

        private static Upload[] ParseUploads( int vids ) {
            var movs = new Upload[ vids ];
            for ( int i = 0; i < vids; i++ ) {
                var r = Console.In.ReadLine().Split( ' ' );
                movs[i] = new Upload {
                        Index = i,
                        Date = int.Parse( r[ 0 ] ),
                        Length = int.Parse( r[ 1 ] ),
                        Complete = -1
                };
            }
            return movs;
        }
        private static void Impl2( Input input ) {
            long[] servers = new long[ input.Servers ];

            long current = 0;
            long start = 0;

            var uploads = input.uploads;
            var length = uploads.Length;

            for ( int index = 0; index < length; index++ ) {
                var upload = uploads[ index ];
                l:
                start = Math.Max( start, upload.Date );
                if ( servers[ current ] <= start ) {
                    upload.Complete = servers[ current ] = Math.Max( servers[ current ], upload.Date) + upload.Length;
                    current = ( current + 1 ) % input.Servers;
                    continue;
                }
                Array.Sort( servers ); //нужно что-то более эффективное, но пока сойдёт
                start = servers[ 0 ];
                current = 0;
                goto l;
            }
        }

        private static void swap( long[] servers, long i, long i1 ) {
            var t = servers[ i ];
            servers[ i ] = servers[ i1 ];
            servers[ i1 ] = t;
        }
    }
    class Input {
        public int Servers;
        public Upload[] uploads;
    }
    class Upload {
        public int Index;
        public long Date;
        public long Length;
        public long Complete;
    }
}
