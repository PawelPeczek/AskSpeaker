using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public abstract class RequestMaker {

		public BaseRequest PrepareRequest (){
			PrintMethodDialogHeader ();
			return MakeRequest ();
		}

		protected abstract BaseRequest MakeRequest ();

		protected string ProceedStringValueGettingDialog(string valueName){
			Console.Write ($"Enter {valueName}: ");
			string result = Console.ReadLine ();
			Console.WriteLine ();
			return result;
		}

		protected bool ProceedBoolValueGettingDialog(string valueName){
			bool result = false;
			Console.WriteLine ("Enter [y] if {valueName} property should be set true or anythhing otherwise: ");
			char readChar = Console.ReadKey().KeyChar;
			Console.WriteLine ();
			if (readChar == 'y')
				result = true;
			return result;
		}

		protected int TryParseID(string value, string IDName){
			int result;
			if (!int.TryParse (value, out result) || result < 0)
				throw new ArgumentException ($"Value is not a valid {IDName}");
			return result;
		}

		protected void PrintMethodDialogHeader(){
			Console.WriteLine ($"{GetType().Name} editor:");
		}
	}
}

