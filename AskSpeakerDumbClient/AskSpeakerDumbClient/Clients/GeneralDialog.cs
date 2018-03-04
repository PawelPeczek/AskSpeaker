using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using Newtonsoft.Json;

namespace AskSpeakerDumbClient.Clients {
	public abstract class GeneralDialog<T> {
		
		public ConsoleColor orignalForegroundColor = Console.ForegroundColor;
		protected const string EXIT_STRING = "quit";
		protected Array RequestTypes;
		protected RequestTracker RequestTracker;

		public GeneralDialog(){
			RequestTracker = new RequestTracker ();
			RequestTypes = Enum.GetValues(typeof(T));
		}

		public abstract void StartDialog ();

		public int AddRequestToTracker(BaseRequest request){
			return RequestTracker.AddRequest (request);
		}

		protected void StartUserDialogLoop(){
			string option;
			do {
				PrintRequestsHeader();
				ListAllRequests();
				PrintProgramExitOptionNotif();
				option = ProceedOptionChoiceDialog();
				HandleUserChoice(option);
			} while(option.ToLower() != EXIT_STRING);
		}

		private void HandleUserChoice(string option){
			try {
				ProceedUserChoice(option);
			} catch(ArgumentException ex){
				Console.WriteLine ($"Some error interupted dialog. Details:\n{ex.Message}");
			}
			Console.WriteLine ("\nPress [Any key] to continue.\n");
			Console.ReadKey ();
		}

		private void ProceedUserChoice(string option){
			if (option.ToLower () == EXIT_STRING)
				return;
			int choosenNumber;
			if (IsValidOptionNumber(option, out choosenNumber)) {
				T choosenType = (T)RequestTypes.GetValue(choosenNumber);
				ExecuteUserCommand (choosenType);
			} else {
				ColorPrintMessage ("Invalid request number!\n", ConsoleColor.Red);
			}
		}

		protected abstract string ExecuteUserCommand (T choosenType);

		private bool IsValidOptionNumber(string option, out int choosenNumber){
			return (int.TryParse (option, out choosenNumber) && choosenNumber >= 0 && choosenNumber < RequestTypes.Length);
		}


		private void ListAllRequests(){
			int i = 0;
			foreach(var type in RequestTypes){
				Console.WriteLine ($"{i}. {type.ToString()}");
				i++;
			}
		}

		protected string ReadMaskedPassword(){
			ConsoleKeyInfo key;
			String pass = "";
			do {
				key = Console.ReadKey(true);
				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)	{
					pass += key.KeyChar;
					Console.Write("*");
				} else {
					if (key.Key == ConsoleKey.Backspace && pass.Length > 0)	{
						pass = pass.Substring(0, (pass.Length - 1));
						Console.Write("\b \b");
					}
				}
			} while (key.Key != ConsoleKey.Enter);
			return pass;
		}
	
		protected void ColorPrintMessage(string message, ConsoleColor color){
			Console.ForegroundColor = color;
			Console.WriteLine (message);
			Console.ForegroundColor = orignalForegroundColor;
		}

		protected string ProceedOptionChoiceDialog(){
			Console.Write ("[Your chioce] ");
			string result = Console.ReadLine();
			Console.WriteLine ();
			return result;
		}

		protected void PrintRequestsHeader(){
			Console.WriteLine ("\n*****************************************");
			Console.WriteLine ("*\t\tSELECT REQUEST\t\t*");
			Console.WriteLine ("*****************************************");
			ColorPrintMessage ("Select the number of request.", ConsoleColor.Green);
		}

		protected void PrintProgramExitOptionNotif(){
			Console.Write ("\nTo exit the program type: ");
			ColorPrintMessage (EXIT_STRING, ConsoleColor.Yellow);
		}

	}
}

