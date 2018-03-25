using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOwnershipChangeBroadcast : BroadcastWIthEventHash {

		public EventOwnershipChangeBroadcast(){
			Broadcast = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public string NewOwnerName {
			get;
			set;
		}

        [JsonIgnore]
        public int NewOwnerId {
            get;
            set;
        }

	}
}

