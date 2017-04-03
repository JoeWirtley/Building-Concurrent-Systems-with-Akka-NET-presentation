﻿using System;
using Akka.Actor;
using Akka.Event;

namespace AkkaGuardian {
   public class PeterQuillActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );

      public PeterQuillActor() {
         Receive<string>( message => {
            _log.Info( $"Peter Quill received message '{message}'" );
            Sender.Tell( RandomReply( message) );
         } );
      }

      private string RandomReply( string message ) {
         if ( message.Equals( "star lord", StringComparison.CurrentCultureIgnoreCase ) ) {
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