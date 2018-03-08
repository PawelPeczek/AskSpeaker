using System;
using WebSocket4Net;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.EntityFramework.Entities;
using System.IO;
using AskSpeakerDumbClient.Clients.AdministratorClient;
using AskSpeakerDumbClient.Clients;
using AskSpeakerDumbClient.Clients.SubscriberClient;
using AskSpeakerServer.BackEnd.SubscriberRequests;

namespace AskSpeakerDumbClient {
	class MainClass {
		public static void Main (string[] args) {

			Console.WriteLine ("Client start");
			try {
				RunningMode mode = ProceedSelectionModeDialog();
				DispatchRunningMode(mode);
			} catch (ApplicationException ex){
				Console.WriteLine ($"Program stopped because of some issue. Details:\n{ex.Message}");
			}
		}

		private static RunningMode ProceedSelectionModeDialog(){
			while (true) {
				PrintHeader ();
				PrintOptions ();
				int userChoice;
				try {
					userChoice = TakeUserInput();
					Console.WriteLine ("User choice: " + userChoice);
					return ConvertUserChioceIntoMode(userChoice);
				} catch (ArgumentException ex){
					Console.WriteLine (ex.Message);
				}
			}
		}

		private static void PrintHeader(){
			Console.WriteLine ("**********\tAskSpeakerDumbClient\t**********");
			Console.WriteLine ("**********\t      TEST APP      \t**********");
		}

		private static void PrintOptions(){
			Console.WriteLine ("Choose number of option:");
			Console.WriteLine ("1 - AdminMode");
			Console.WriteLine ("2 - UserMode");
			Console.WriteLine ("3 - Quit");
		} 

		private static int TakeUserInput(){
			string userChioce = Console.ReadLine ();
			int value;
			if (!int.TryParse (userChioce, out value))
				throw new ArgumentException ("Non-number option given.");
			if (value < 1 || value > 3)
				throw new ArgumentException ("Invalid number of option.");
			return value;
		}

		private static RunningMode ConvertUserChioceIntoMode(int userChoice){
			RunningMode mode = RunningMode.Quit;
			switch (userChoice) {
				case 1:
					mode = RunningMode.AdminMode;
					break;
				case 2:
					mode = RunningMode.SubscriberMode;
					break;
			}
			return mode;
		}

		private static void DispatchRunningMode(RunningMode mode){
			switch (mode) {
				case RunningMode.AdminMode:
					StartDialog<AdminDialog, AdminRequestTypes> ();
					break;
				case RunningMode.SubscriberMode:
					StartDialog<SubscriberDialog, SubscriberRequestTypes> ();
					break;
				default:
					break;
			}
		}

		private static void StartDialog<T, K>() where T : GeneralDialog<K>, new() {
			T dialog = new T ();
			dialog.StartDialog ();
		}

	}
}
