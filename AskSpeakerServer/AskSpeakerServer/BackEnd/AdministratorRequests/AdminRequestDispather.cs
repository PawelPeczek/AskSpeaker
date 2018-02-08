using System;
using System.Collections.Generic;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public static class AdminRequestDispather {
		public static string Dispath
		(AdminRequestTypes reqType, string message, IDictionary<Object, Object> container) {
			switch (reqType) {
				default:
					throw new NotImplementedException();
			}
		}
	}
}

