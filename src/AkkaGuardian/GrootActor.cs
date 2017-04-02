using Akka.Actor;
using Akka.Event;

namespace AkkaGuardian {
   public class GrootActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );

      public GrootActor() {
         Receive<string>( message => {
            _log.Info( $"Groot received message '{message}'" );
            Sender.Tell( "I am Groot" );
         } );
      }
   }
}