﻿using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	public class EventsInfoRenevRequestMaker : RequestMaker<AdminRequestTypes> {
		
		protected override BaseRequest MakeRequest () {
			return new EventsListRequest ();
		}


		protected override void PrintMethodDialogHeader(){
			Console.WriteLine ($"{GetType ().Name} does not require configuration. [ALL DONE]");
		}
		
	}
}

