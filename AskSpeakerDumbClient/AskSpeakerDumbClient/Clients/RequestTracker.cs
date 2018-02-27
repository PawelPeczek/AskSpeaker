using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerDumbClient {
	public class RequestTracker {

		private int NextID;

		private Dictionary<int, BaseRequest> Requests;

		public RequestTracker(){
			NextID = 0;
			Requests = new Dictionary<int, BaseRequest> ();
		}

		public void AddRequest(BaseRequest request){
			// Zero and in.MaxValue numbers
			if (Requests.Count > int.MaxValue)
				throw new ApplicationException ("Tracker is full of unresponsed requests.");
			Requests.Add (NextID, request);
			if (NextID == int.MaxValue)
				NextID = 0;
			else
				NextID++;
		}

		public int getBumberOfPendingRequests(){
			return Requests.Count;
		}
	}
}

