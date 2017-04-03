using System.Linq;
using Akka.Actor;

namespace AkkaGuardian {
   public static class AkkaExtensions {
      public static string ActorName( this IActorRef actor ) {
         return actor.Path.Elements.Last();
      }
   }
}