using System;
using Akka.Actor;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   class Program {

      static void Main( string[] args ) {
         AdjustConsoleWindow();

         ActorSystem system = ActorSystem.Create( "MyActorSystem" );

         system.ActorOf<GrootActor>( "groot" );
         system.ActorOf<PeterQuillActor>( "peter" );

         IActorRef yondu = system.ActorOf<YonduActor>( "yondu" );

         InputHandler handler = new InputHandler();

         object message;
         while ( handler.GetUserInput( out message ) ) {
            if ( message is TellMessage ) {
               TellMessage tellMessage = ( TellMessage ) message;
               system.ActorSelection( $"/user/{tellMessage.Who}" ).Tell( tellMessage.What );
            } if ( message is CreateRavagerMessage ) {
               yondu.Tell( message );
            }
            if ( message is ListRavagersMessage ) {
               string ravagerNames = yondu.Ask<string>( message ).Result;
               Console.WriteLine( ravagerNames );
            }
         }
      }

      private static void AdjustConsoleWindow() {
         Console.SetWindowSize( Math.Min( 150, Console.LargestWindowWidth ), Math.Min( 40, Console.LargestWindowHeight ) );
      }
   }
}