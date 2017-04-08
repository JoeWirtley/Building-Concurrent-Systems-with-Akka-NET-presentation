using System;
using Akka.Actor;

namespace AkkaGuardian {
   public class InputHandler {
      public InputHandler() {
         Console.WriteLine( "Type 'exit' and press enter to exit" );
         Console.WriteLine( "Type 'tell {actor name} message' to send a message to an actor" );
         Console.WriteLine( "For example: tell groot Hello" );
         Console.WriteLine( "----------------------------------------------------------------------------------" );
      }

      internal bool GetValidInput( out Input input ) {
         input = new Input();
         do {
            string inputText = Console.ReadLine();
            if ( inputText.Contains( "tell" ) ) {
               input = ParseTell( inputText );
            } else if ( inputText.Contains( "exit" ) ) {
               break;
            }
            return true;
         } while ( true );
         return false;
      }

      private Input ParseTell( string inputText ) {
         int firstSpace = inputText.IndexOf( " " );
         inputText = inputText.Substring( firstSpace + 1 );
         firstSpace = inputText.IndexOf( " " );
         return new SpeakMessage( inputText.Substring( 0, firstSpace  ), inputText.Substring( firstSpace + 1 ) );
      }
   }

   public class Input {
   }
}