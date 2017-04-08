using Akka.Actor;
using Akka.Event;

namespace AkkaGuardian {
   public class NarratorActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );

      public NarratorActor() {
         Receive<SpeakMessage>( message => Context.System.ActorSelection( $"/user/{message.Who}" ).Tell( message.What ) );
         Receive<string>( message => _log.Info( $"Narrator heard '{message}' from {Sender.ActorName()}" ) );
      }
   }

   public class SpeakMessage: Input {
      public string Who { get; }
      public string What { get; }

      public SpeakMessage( string who, string what ) {
         Who = who;
         What = what;
      }
   }
}