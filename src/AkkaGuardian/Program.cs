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
         ParsedLine input = new ParsedLine();
         while ( !input.ShouldExit ) {
            string inputText = Console.ReadLine();
            input = new ParsedLine( inputText, _actors);
            if ( input.ActorRef != null ) {
               NarratorActor.SpeakTo message = new NarratorActor.SpeakTo( input.ActorRef, input.Phrase );
               _narrator.Tell( message );
            }
         }
      }

      private static void RegisterActor( IActorRef actor ) {
         _actors.Add( actor.ActorName(), actor );
      }

   }
   public class ParsedLine {
      public bool ShouldExit { get; }
      public IActorRef ActorRef { get; }
      public string Phrase { get; }

      public ParsedLine() {
         ShouldExit = false;
      }

      public ParsedLine( string input, Dictionary<string, IActorRef> actors ) {
         string[] parts = input.Split( '-' );
         if ( parts.Length == 2 ) {
            IActorRef actorRef;
            if ( actors.TryGetValue( parts[ 0 ], out actorRef ) ) {
               ActorRef = actorRef;
               Phrase = parts[ 1 ];
            }
         } else if ( input == "exit" ) {
            ShouldExit = true;
         }
      }
   }

   public static class AkkaExtensions {
      public static string ActorName( this IActorRef actor ) {
         return actor.Path.Elements.Last();
      }
   }
}