using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace AkkaGuardian {
   class Program {
      private static Dictionary<string,IActorRef> _actors = new Dictionary<string, IActorRef>();

      static void Main( string[] args ) {
         ActorSystem system = ActorSystem.Create( "MyActorSystem" );

         IActorRef groot = system.ActorOf<GrootActor>( "groot" );


         RegisterActor( groot );

         HandleCommands();
      }


      private static void HandleCommands() {
         Console.WriteLine( "Type 'exit' and press enter to exit" );
         Console.WriteLine( "Type actor name hyphen message to send a message to an actor" );
         Console.WriteLine( "For example: groot-Hello" );
         string input = "";
         while ( input != "exit" ) {
            input = Console.ReadLine();
            string[] parts = input.Split( '-' );
            if ( parts.Length == 2 ) {
               IActorRef actorRef;
               if (_actors.TryGetValue( parts[0], out actorRef ) ) {
                  actorRef.Tell( parts[1 ]  );
               }
            }
         }
      }

      private static void RegisterActor( IActorRef actor ) {
         _actors.Add( actor.Path.Elements.Last(), actor );
      }

   }
}