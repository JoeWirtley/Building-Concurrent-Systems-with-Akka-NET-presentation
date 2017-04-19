using Akka.Actor;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class RavagerActor: ReceiveActor {
      public RavagerActor() {
         Receive<TellMessage>( message => DisplayHelper.Say( $"Ravager says 'I am {Self.ActorName()}'" ) );
      }
   }
}