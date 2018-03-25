using System;
namespace AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction {
  public interface ResponseMakerFactory<T> {
    ResponseMaker<T> ProvideResponseMaker(PreProcessedMessage<T> request);
  }
}
