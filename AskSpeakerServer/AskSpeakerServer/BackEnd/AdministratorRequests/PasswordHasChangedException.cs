using System;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class PasswordHasChangedException : Exception {
		public PasswordHasChangedException() : base() {
		}

		public PasswordHasChangedException(string message) : base(message) {
		}
	}
}

