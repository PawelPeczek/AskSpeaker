using System;
using AskSpeakerServer.BackEnd;

namespace AskSpeakerServer {
  class MainClass {
    public static void Main(string[] args) {
      //SystemInit.CreateRootUser();
      Server server = new Server();
      server.Start();
      Console.ReadKey();
      server.Stop();
    }
  }
}
