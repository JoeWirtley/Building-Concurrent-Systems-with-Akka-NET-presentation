using System;
using Akka.Actor;

namespace AkkaGuardian {
   class Program {
      static void Main( string[] args ) {
         ActorSystem system = ActorSystem.Create( "MyActorSystem" );

         IActorRef groot = system.ActorOf<GrootActor>( "groot" );

         groot.Tell( "Hello"  );


         Console.WriteLine( "Press enter to continue" );
         Console.ReadLine();
      }
   }
}