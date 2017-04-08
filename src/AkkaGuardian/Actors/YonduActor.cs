using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Event;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class YonduActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );
      private readonly List<IActorRef> _ravagers = new List<IActorRef>();

      public YonduActor() {
         Receive<string>( message => {
            _log.Info( $"Yondu received message '{message}'" );
            Sender.Tell( RandomReply( message) );
         } );
         Receive<CreateRavagerMessage>( message => _ravagers.Add(Context.ActorOf<RavagerActor>()) );
         Receive<ListRavagersMessage>( message => 
            Sender.Tell(  string.Join( ", ", _ravagers.Select( r => r.ActorName() ).ToArray()) )
          );
      }

      private string RandomReply( string message ) {
         string[] replies = {
            "We're Ravagers, we got a code.",
            "I may be as pretty as an angel, but I sure as hell ain't one."
         };

         return replies[ new Random().Next( 0, replies.Length ) ];
      }
   }
}