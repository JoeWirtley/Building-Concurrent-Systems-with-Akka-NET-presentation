using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace AkkaGuardian {
   class Program {
      private static Dictionary<string,IActorRef> _actors = new Dictionary<string, IActorRef>();
      private static IActorRef _narrator;

      static void Main( string[] args ) {
         ActorSystem system = ActorSystem.Create( "MyActorSystem" );

         _narrator = system.ActorOf<NarratorActor>( "narator" );

         IActorRef groot = system.ActorOf<GrootActor>( "groot" );
         IActorRef peter = system.ActorOf<PeterQuillActor>( "peter" );

         RegisterActor( groot );
         RegisterActor( peter );

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
                  NarratorActor.SpeakTo message = new NarratorActor.SpeakTo( actorRef, parts[1 ] );
                  _narrator.Tell( message );
               }
            }
         }
      }

      private static void RegisterActor( IActorRef actor ) {
         _actors.Add( actor.ActorName(), actor );
      }

   }

   public static class AkkaExtensions {
      public static string ActorName( this IActorRef actor ) {
         return actor.Path.Elements.Last();
      }
   }
}