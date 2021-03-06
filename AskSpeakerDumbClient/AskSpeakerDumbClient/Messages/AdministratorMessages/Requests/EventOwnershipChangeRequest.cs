﻿using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventOwnershipChangeRequest : RequestWithEventHash {

		public EventOwnershipChangeRequest(){
			Request = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public string NewOwnerUsername {
			get;
			set;
		}
	}
}

