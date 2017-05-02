namespace AkkaGuardian.Messages {
   public class TellMessage {
      public string Who { get; }
      public string What { get; }

      public TellMessage( string who, string what ) {
         Who = who;
         What = what;
      }

      public bool Contains( string toFind ) {
         return What.ToLower().Contains( toFind.ToLower() );
      }
   }
}