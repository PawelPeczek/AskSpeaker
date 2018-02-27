using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerDumbClient.Clients.AdministratorClient  {
	public class AdminRequestMaker {

		private AdminRequestTypes SelectedType;
		private AdminDialog Dialog;

		public AdminRequestMaker (AdminRequestTypes type, AdminDialog dialog) {
			SelectedType = type;
			Dialog = dialog;
		}

		public BaseRequest PrepareRequest(){
			BaseRequest result = null;
			switch (SelectedType) {
				case AdminRequestTypes.EventChangeOwnership:
					result = PrepareChangeOvnershipRequest ();
					break;
				case AdminRequestTypes.EventReOpen:
					result = PrepareOpenRequest ();
					break;
				case AdminRequestTypes.EventClose:
					result = PrepareCloseRequest ();
					break;
				case AdminRequestTypes.EventCreate:
					result = PrepareEventCreateRequest ();
					break;
				case AdminRequestTypes.EventEdit:
					result = PrepareEventEditRequest ();
					break;
			}
			Dialog.gerTequestTracker ().AddRequest (result);
			return result;
		}
			
		private EventOwnershipChangeRequest PrepareChangeOvnershipRequest(){
			Console.WriteLine ("Creating EventOwnershipChangeRequest editor:");
			EventOwnershipChangeRequest result = new EventOwnershipChangeRequest ();
			string eventID = ProceedValueGettingDialog("EventID");
			result.EventID = TryParseID(eventID, "EventID");
			Console.Write ("Enter NewOwnerID: ");
			string newOwnerID = ProceedValueGettingDialog("NewOwnerID");
			Console.WriteLine ();
			result.newOwnerID = TryParseID(newOwnerID, "NewOwnerID");
			return result;
		}


		private EventOpenCloseRequest PrepareOpenRequest(){
			Console.WriteLine ("Creating EventReOpenRequest editor:");
			EventOpenCloseRequest result =  PrepareOpenOrCloseRequest ();
			result.Request = AdminRequestTypes.EventReOpen.GetRequestString ();
		}

		private EventOpenCloseRequest PrepareCloseRequest(){
			Console.WriteLine ("Creating EventCloseRequest editor:");
			EventOpenCloseRequest result = PrepareOpenOrCloseRequest ();
			result.Request = AdminRequestTypes.EventClose.GetRequestString ();
		}

		private EventOpenCloseRequest PrepareOpenOrCloseRequest(){
			EventOpenCloseRequest result = new EventOpenCloseRequest ();
			Console.Write ("Enter EventID: ");
			string eventID = Console.ReadLine ();
			Console.WriteLine ();
			result.EventID = TryParseID(eventID, "EventID");
			return result;
		}

		private EventEditCreateRequest PrepareEventCreateRequest(){
			Console.WriteLine ("Creating EventCreateRequest editor:");
			EventEditCreateRequest result = PrepareEventEditCreateRequest();
			result.Request = AdminRequestTypes.EventCreate.GetRequestString ();
			return result;
		}

		private EventEditCreateRequest PrepareEventEditRequest(){
			Console.WriteLine ("Creating EventEditRequest editor:");
			EventEditCreateRequest result = PrepareEventEditCreateRequest();
			result.Request = AdminRequestTypes.EventEdit.GetRequestString ();
			return result;
		}
			

		private EventEditCreateRequest PrepareEventEditCreateRequest(){
			EventEditCreateRequest result = new EventEditCreateRequest ();
			result.Event = new Events ();
			result.Event.EventName = ProceedValueGettingDialog ("EventName");
			result.Event.EventDesc = ProceedValueGettingDialog ("EventDesc");
			result.Event.SpeakerName = ProceedValueGettingDialog("SpeakerName");
			result.Event.SpeakerSurname = ProceedValueGettingDialog ("SpeakerSurname");
			result.Event.Closed = ProceedBoolValueGettingDialog ("Closed");
		}

		private string ProceedValueGettingDialog(string valueName){
			Console.Write ($"Enter {valueName}: ");
			string result = Console.ReadLine ();
			Console.WriteLine ();
			return result;
		}

		private bool ProceedBoolValueGettingDialog(string valueName){
			bool result = false;
			Console.WriteLine ("Enter [y] if {valueName} property should be set true or anythhing otherwise: ");
			char readChar = Console.ReadKey ();
			Console.WriteLine ();
			if (readChar == 'y')
				result = true;
			return result;
		}

		private int TryParseID(string value, string IDName){
			int result;
			if (!int.TryParse (value, out result) || result < 0)
				throw new ArgumentException ($"Value is not a valid {IDName}");
			return result;
		}
	}
}

