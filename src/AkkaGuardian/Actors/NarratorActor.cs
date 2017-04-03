using Akka.Actor;
using Akka.Event;

namespace AkkaGuardian {
   public class NarratorActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );

      public NarratorActor() {
         Receive<SpeakTo>( message => message.Who.Tell( message.What ) );
         Receive<string>( message => _log.Info( $"Narrator heard '{message}' from {Sender.ActorName()}" ) );
      }
   }

   public class SpeakTo {
      public IActorRef Who { get; }
      public string What { get; }

      public SpeakTo( IActorRef who, string what ) {
         Who = who;
         What = what;
      }
   }
}