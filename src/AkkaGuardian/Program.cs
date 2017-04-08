﻿using System;
using Akka.Actor;

namespace AkkaGuardian {
   class Program {
      private static IActorRef _narrator;

      static void Main( string[] args ) {
         AdjustConsoleWindow();

         ActorSystem system = ActorSystem.Create( "MyActorSystem" );

         _narrator = system.ActorOf<NarratorActor>( "narator" );

         IActorRef groot = system.ActorOf<GrootActor>( "groot" );
         IActorRef peter = system.ActorOf<PeterQuillActor>( "peter" );


         InputHandler handler = new InputHandler();
         Input input;
         while ( handler.GetValidInput( out input ) ) {
            if ( input is SpeakMessage ) {
               _narrator.Tell( input );
            }
         }
      }

      private static void AdjustConsoleWindow() {
         Console.SetWindowSize( Math.Min( 150, Console.LargestWindowWidth ), Math.Min( 40, Console.LargestWindowHeight ) );
      }
   }
}