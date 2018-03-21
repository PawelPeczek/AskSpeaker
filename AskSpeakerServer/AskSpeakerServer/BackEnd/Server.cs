using System;

namespace AskSpeakerServer.BackEnd {
	public class Server {

		private AdministratorServer AdminServer = new AdministratorServer ();
		private SubscriberServer SubscribersServer = new SubscriberServer ();

		public Server () {
			AdminServer.ProvideSubscriberServer (SubscribersServer);
			SubscribersServer.ProvideAdministratorServer (AdminServer);
		}

		public void Start(){
			AdminServer.Start ();
			SubscribersServer.Start ();
			SubscribersServer.Synchro.Set ();
			AdminServer.Synchro.Set ();
		}

		public void Stop(){
			AdminServer.Stop ();
			SubscribersServer.Stop ();
		}
	}
}

