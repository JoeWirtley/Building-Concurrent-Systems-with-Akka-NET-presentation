using System;
using Akka.Actor;

namespace AkkaGuardian {
   public class GrootActor: ReceiveActor {
      public GrootActor() {
         Receive<string>( message => DisplayHelper.Say( "Groot says 'I am Groot'" ) );
      }
   }
}