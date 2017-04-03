using System.Collections.Generic;
using Akka.Actor;

namespace AkkaGuardian {
   public class Input {
      public bool ShouldExit { get; }
      public bool HasValidInput { get; }
      public IActorRef ActorRef { get; }
      public string Phrase { get; }

      public Input() {
         ShouldExit = false;
      }

      public Input( string input, Dictionary<string, IActorRef> actors ) {
         HasValidInput = false;
         string[] parts = input.Split( '-' );
         if ( parts.Length == 2 ) {
            IActorRef actorRef;
            if ( actors.TryGetValue( parts[ 0 ], out actorRef ) ) {
               ActorRef = actorRef;
               Phrase = parts[ 1 ];
               HasValidInput = true;
            }
         } else if ( input == "exit" ) {
            ShouldExit = true;
         }
      }
   }
}