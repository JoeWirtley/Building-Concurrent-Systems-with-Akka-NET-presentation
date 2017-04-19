using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using AkkaGuardian.Messages;

namespace AkkaGuardian {
   public class YonduActor: ReceiveActor {
      private readonly List<IActorRef> _ravagers = new List<IActorRef>();
      private int _numberOfRavagers = 0;

      public YonduActor() {
         Receive<TellMessage>( message =>
            DisplayHelper.Say( RandomReply() )
         );
         Receive<CreateRavagerMessage>( message => {
               _numberOfRavagers = _numberOfRavagers + 1;
               var ravager = Context.ActorOf<RavagerActor>( "ravager" + _numberOfRavagers );
               Context.Watch( ravager );
               _ravagers.Add( ravager );
            }
         );
         Receive<KillRavagersMessage>( message => {
               foreach ( var ravager in _ravagers ) {
                  Context.Stop( ravager );
               }
            }
         );
         Receive<ListRavagersMessage>( message => {
               string ravagerList;
               if ( _ravagers.Count == 0 ) {
                  ravagerList = "No ravagers";
               } else {
                  ravagerList = string.Join( ", ", _ravagers.Select( r => r.ActorName() ).ToArray() );
               }
               Sender.Tell( ravagerList );
            }
         );
         Receive<Terminated>( message => {
               if ( _ravagers.Contains( Sender ) ) {
                  _ravagers.Remove( Sender );
               }
            }
         );
      }

      private string RandomReply() {
         string[] replies = {
            "We're Ravagers, we got a code.",
            "I may be as pretty as an angel, but I sure as hell ain't one."
         };

         return replies[ new Random().Next( 0, replies.Length ) ];
      }
   }
}