using System;
using static System.Console;

namespace AkkaGuardian {
   public static class DisplayHelper {
      public static void Say( string toSay ) {
         using ( new ScopedConsoleColor( ConsoleColor.Green ) ) {
            WriteLine( toSay );
         }
      }

      public static void List( string list ) {
         using ( new ScopedConsoleColor( ConsoleColor.White ) ) {
            WriteLine( list );
         }
      }

      public static void Warn( string warning ) {
         using ( new ScopedConsoleColor( ConsoleColor.Yellow ) ) {
            WriteLine( warning );
         }
      }
   }

   public class ScopedConsoleColor:IDisposable {
      private ConsoleColor _oldColor;

      public ScopedConsoleColor( ConsoleColor newColour ) {
         _oldColor = Console.ForegroundColor;

         ForegroundColor = newColour;
      }

      public void Dispose() {
         ForegroundColor = this._oldColor;
      }
   }
}