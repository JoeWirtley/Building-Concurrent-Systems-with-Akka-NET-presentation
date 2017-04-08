using Akka.Actor;
using Akka.Event;

namespace AkkaGuardian {
   public class RavagerActor: ReceiveActor {
      private readonly ILoggingAdapter _log = Logging.GetLogger( Context );
      public RavagerActor() {
         _log.Info( "Ravager created" );
      }
   }
}