using Akka.Actor;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class GrootActor: ReceiveActor {
      public GrootActor() {
         Receive<TellMessage>( message => DisplayHelper.Say( "Groot says 'I am Groot'" ) );
      }
   }
}