using Akka.Actor;
using Akka.Event;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class NarratorActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );

      public NarratorActor() {
         Receive<TellMessage>( message => Context.System.ActorSelection( $"/user/{message.Who}" ).Tell( message.What ) );
         Receive<string>( message => _log.Info( $"Narrator heard '{message}' from {Sender.ActorName()}" ) );
      }
   }
}