using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerDumbClient.Clients {
	public class RequestTracker {

		private int NextID;

		private Dictionary<int, BaseRequest> Requests;

		public RequestTracker(){
			NextID = 0;
			Requests = new Dictionary<int, BaseRequest> ();
		}

		public int getBumberOfPendingRequests(){
			return Requests.Count;
		}

		public int AddRequest(BaseRequest request){
			// Zero and in.MaxValue numbers
			if (Requests.Count > int.MaxValue)
				throw new ApplicationException ("Tracker is full of unresponsed requests.");
			Requests.Add (NextID, request);
			if (NextID == int.MaxValue) {
				NextID = 0;
			} else {
				PickNextID ();
			}
			return NextID;
		}

		private void PickNextID(){
			NextID++;
			while (Requests.ContainsKey (NextID)) {
				NextID++;
			}
		}

	}
}

