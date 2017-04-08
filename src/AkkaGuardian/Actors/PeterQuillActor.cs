using System;
using Akka.Actor;

namespace AkkaGuardian {
   public class PeterQuillActor: ReceiveActor {
      public PeterQuillActor() {
         Receive<string>( message => DisplayHelper.Say( $"Peter says '{RandomReply( message )}'" ) );
      }

      private string RandomReply( string message ) {
         if ( message == "star lord" ) {
            return "Finally!";
         }

         string[] replies = {
            "Well that's just fascinating",
            "That's gonna wear real thin, real fast, bud",
            "I have a plan"
         };

         return replies[ new Random().Next( 0, replies.Length ) ];
      }
   }
}