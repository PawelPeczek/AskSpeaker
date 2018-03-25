using System;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests {
  public class SubscriberResponseMakerFactory : ResponseMakerFactory<SubscriberRequestTypes> {
    public ResponseMaker<SubscriberRequestTypes> ProvideResponseMaker(PreProcessedMessage<SubscriberRequestTypes> request) {
      ResponseMaker<SubscriberRequestTypes> result = null;
      switch(request.RequestType) {
        case SubscriberRequestTypes.QuestionsRequest:
          result = new QuestionsRequestResponseMaker();
          break;
        case SubscriberRequestTypes.VoteRequest:
          result = new VoteRequestResponseMaker();
          break;
        case SubscriberRequestTypes.QuestionAddRequest:
          result = new QuestionAddRequestResponseMaker();
          break;
      }
      return result;
    }
  }
}
