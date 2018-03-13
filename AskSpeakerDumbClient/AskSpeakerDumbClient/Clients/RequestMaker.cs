using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerDumbClient.Clients {
	public abstract class RequestMaker<T> {

		public BaseRequest PrepareRequest (){
			PrintMethodDialogHeader ();
			return MakeRequest ();
		}

		protected abstract BaseRequest MakeRequest ();

		protected virtual void PrintMethodDialogHeader(){
			Console.WriteLine ($"{GetType().Name} editor:");
		}

		protected string ProceedStringValueGettingDialog(string valueName){
			Console.Write ($"Enter {valueName}: ");
			string result = Console.ReadLine ();
			return result;
		}

		protected bool ProceedBoolValueGettingDialog(string valueName){
			bool result = false;
			Console.WriteLine ($"Enter [y] if {valueName} property should be set true or anythhing otherwise: ");
			char readChar = Console.ReadKey().KeyChar;
			Console.WriteLine ();
			if (readChar == 'y')
				result = true;
			return result;
		}

	}
}

