using System;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class InputHandler {
      public InputHandler() {
         Console.WriteLine( "Available commands" );
         Console.WriteLine( "  tell {actor} {text} = say {message} to {actor}" );
         Console.WriteLine( "  create ravager      = create a new ravager" );
         Console.WriteLine( "  list ravagers       = list all ravagers" );
         Console.WriteLine( "  kill ravagers       = remove all ravagers" );
         Console.WriteLine( "  exit                = get outta here" );
         Console.WriteLine( "----------------------------------------------------------------------------------" );
      }

      internal bool GetUserInput( out object message ) {
         message = new object();
         do {
            try {
               string inputText = Console.ReadLine();
               if ( inputText.Contains( "tell" ) ) {
                  message = ParseTell( inputText );
               } else if ( inputText.Contains( "create" ) && inputText.Contains( "ravager" ) ) {
                  message = new CreateRavagerMessage();
               } else if ( inputText.Contains( "list" ) && inputText.Contains( "ravagers" ) ) {
                  message = new ListRavagersMessage();
               } else if ( inputText.Contains( "kill" ) && inputText.Contains( "ravagers" ) ) {
                  message = new KillRavagersMessage();
               } else if ( inputText.Contains( "exit" ) ) {
                  break;
               } else {
                  DisplayHelper.Warn( "What?" );
               }
               return true;
            } catch {
               // probably a parsing error, just go on with life
               DisplayHelper.Warn( "What?" );
            }
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