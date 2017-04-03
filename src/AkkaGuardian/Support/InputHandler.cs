using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace AkkaGuardian {
   public class InputHandler {
      private static readonly Dictionary<string, IActorRef> _actors = new Dictionary<string, IActorRef>();

      public InputHandler( params IActorRef[] actors ) {
         RegisterActors( actors );
         Console.WriteLine( "Type 'exit' and press enter to exit" );
         Console.WriteLine( "Type actor name hyphen message to send a message to an actor" );
         Console.WriteLine( "For example: groot-Hello" );
         Console.WriteLine( $"Available actors: {string.Join(", ", actors.Select( actor => actor.ActorName() ))}");
         Console.WriteLine( "----------------------------------------------------------------------------------" );
      }

      internal bool GetValidInput( out Input input ) {
         input = new Input();
         while ( !input.ShouldExit ) {
            string inputText = Console.ReadLine();
            input = new Input( inputText, _actors );
            if ( input.HasValidInput ) {
               return true;
            }
         }
         return false;
      }

      private void RegisterActors( params IActorRef[] actors ) {
         foreach ( var actor in actors ) {
            _actors.Add( actor.ActorName(), actor );
         }
      }
   }
}