using System;

namespace AskSpeakerDumbClient {
	public abstract class GeneralDialog {
		
		public ConsoleColor orignalForegroundColor = Console.ForegroundColor;

		protected const string EXIT_STRING = "quit";

		protected RequestTracker RequestTracker;

		public GeneralDialog(){
			RequestTracker = new RequestTracker ();
		}

		public abstract void StartDialog ();

		public RequestTracker gerTequestTracker(){
			return RequestTracker;
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
	
		protected void ColorPrintMessage(string message, ConsoleColor color, bool withNewLine = true){
			Console.ForegroundColor = color;
			if (withNewLine) Console.WriteLine (message);
			else Console.Write (message);
			Console.ForegroundColor = orignalForegroundColor;
		}

		protected string ProceedOptionChoiceDialog(){
			Console.Write ("[Your chioce] ");
			string result = Console.ReadLine();
			Console.WriteLine ();
			return result;
		}

		protected void PrintRequestsHeader(){
			Console.WriteLine ("*****************************************");
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

