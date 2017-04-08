using System;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class InputHandler {
      public InputHandler() {
         Console.WriteLine( "Type 'exit' and press enter to exit" );
         Console.WriteLine( "Type 'tell {actor name} message' to send a message to an actor" );
         Console.WriteLine( "   For example: tell groot Hello" );
         Console.WriteLine( "Type 'create ravager' to create a new ravager" );
         Console.WriteLine( "----------------------------------------------------------------------------------" );
      }

      internal bool GetUserInput( out object message ) {
         message = new object();
         do {
            string inputText = Console.ReadLine();
            if ( inputText.Contains( "tell" ) ) {
               message = ParseTell( inputText );
            } else if ( inputText.Contains( "create" ) && inputText.Contains( "ravager" ) ) {
               message = new CreateRavagerMessage();
            } else if ( inputText.Contains( "list" ) && inputText.Contains( "ravager" ) ) {
               message = new ListRavagersMessage();
            } else if ( inputText.Contains( "exit" ) ) {
               break;
            }
            return true;
         } while ( true );
         return false;
      }

      private TellMessage ParseTell( string inputText ) {
         int firstSpace = inputText.IndexOf( " " );
         inputText = inputText.Substring( firstSpace + 1 );
         firstSpace = inputText.IndexOf( " " );
         return new TellMessage( inputText.Substring( 0, firstSpace  ), inputText.Substring( firstSpace + 1 ) );
      }
   }
}