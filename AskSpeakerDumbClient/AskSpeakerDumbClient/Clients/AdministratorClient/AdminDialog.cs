using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerDumbClient.Clients.AdministratorClient {
	public class AdminDialog : GeneralDialog {
		public override void StartDialog(){
			Credentials credentials = ProceedCredentialsDialog ();
			//SimpleAdmin adminClient = new SimpleAdmin (login, passwd);
			//adminClient.Open ();
			string option;
			do {
				PrintRequestsHeader();
				ListAllRequests();
				PrintProgramExitOptionNotif();
				option = ProceedOptionChoiceDialog();
				HandleUserChoose(option);
			} while(option.ToLower() != EXIT_STRING);
			//adminClient.Close ();
		}

		private void ListAllRequests(){
			int i = 0;
			foreach(AdminRequestTypes type in Enum.GetValues(typeof(AdminRequestTypes))){
				Console.WriteLine ($"{i}. {type.ToString()}");
				i++;
			}
		}

		private Credentials ProceedCredentialsDialog(){
			Credentials result = new Credentials ();
			Console.Write ("[Login] ");
			result.Login = Console.ReadLine ();
			Console.Write ("[Password] ");
			result.Password = ReadMaskedPassword ();
			Console.WriteLine ();
			return result;
		}

		private void HandleUserChoose(string option){
			if (option.ToLower () != EXIT_STRING) {
				int choosenNumber;
				Array reqTypes = Enum.GetValues(typeof(AdminRequestTypes));
				if (int.TryParse (option, out choosenNumber) && choosenNumber >= 0 && choosenNumber < reqTypes.Length) {
					AdminRequestTypes choosenType = (AdminRequestTypes)reqTypes.GetValue(choosenNumber);
					Console.WriteLine ($"Choosen type: {choosenType.ToString()}");
				} else {
					ColorPrintMessage ("Invalid request number!\n", ConsoleColor.Red);
				}
				Console.WriteLine ("\nPress [Any key] to continue.\n");
				Console.ReadKey ();
			}
		}
	}
}

