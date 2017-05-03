using System;
using Akka.Actor;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   class Program {
      static void Main( string[] args ) {
         AdjustConsoleWindow();

         ActorSystem system = ActorSystem.Create( "guardians" );

         system.ActorOf<GrootActor>( "groot" );
         system.ActorOf<PeterQuillActor>( "peter" );

         IActorRef yondu = system.ActorOf<YonduActor>( "yondu" );

         InputHandler handler = new InputHandler();

         object message;
         while ( handler.GetUserInput( out message ) ) {
            if ( message is TellMessage ) {
               string actorName = ( message as TellMessage ).Who;
               system.ActorSelection( $"/user/{actorName}" ).Tell( message );
            }
            if ( message is CreateRavagerMessage || message is KillRavagersMessage ) {
               yondu.Tell( message );
            }
            if ( message is ListRavagersMessage ) {
               string ravagerNames = yondu.Ask<string>( message ).Result;
               DisplayHelper.List( ravagerNames );
            }
         }
      }

      private static void AdjustConsoleWindow() {
         Console.SetWindowSize( Math.Min( 125, Console.LargestWindowWidth ), Math.Min( 40, Console.LargestWindowHeight ) );
         Console.ForegroundColor = ConsoleColor.Cyan;
      }
   }
}