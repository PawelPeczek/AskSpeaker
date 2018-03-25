using System;
namespace AskSpeakerServer.BackEnd.AdministratorRequests {
  public interface ResponseMakerFactory<T> {
    ResponseMaker<T> MakeResponse(PreProcessedMessage<T> request);
  }
}
