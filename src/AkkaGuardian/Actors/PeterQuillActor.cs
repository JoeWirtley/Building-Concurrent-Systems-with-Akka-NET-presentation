using System;
using Akka.Actor;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class PeterQuillActor: ReceiveActor {
      public PeterQuillActor() {
         Become( PeterState );
      }

      private void PeterState() {
         Receive<TellMessage>( message => {
            if ( message.Contains( "star lord" ) ) {
               Become( StarLordState );
               DisplayHelper.Say( "Peter Says 'Finally!'" );
            } else {
               DisplayHelper.Say( $"Peter says '{RandomReply()}'" );
            }
         } );
      }

      private void StarLordState() {
         Receive<TellMessage>( message => {
            if ( message.Contains( "peter" ) ) {
               Become( PeterState );
               DisplayHelper.Say( $"I am Peter." );
            } else {
               DisplayHelper.Say( $"I am Star-Lord." );
            }
         } );
      }

      private string RandomReply() {
         string[] replies = {
            "Well that's just fascinating",
            "That's gonna wear real thin, real fast, bud",
            "I have a plan"
         };

         return replies[ new Random().Next( 0, replies.Length ) ];
      }
   }
}