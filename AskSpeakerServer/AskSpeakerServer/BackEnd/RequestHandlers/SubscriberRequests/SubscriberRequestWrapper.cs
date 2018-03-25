using System;
using System.Collections.Generic;
using System.Data;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests {
  public class SubscriberRequestWrapper : RequestWrapper<SubscriberRequestTypes> {

    private string Hash;

    public SubscriberRequestWrapper(string hash) : this(null, hash) { }

    public SubscriberRequestWrapper(PreProcessedSubscriberMessage message, string hash) {
      Message = message;
      Hash = hash;
    }

    public override CommunicationChunk HandleStandardRequest() {
      if(Message == null)
        throw new ApplicationException("Internal application error. Code 103");
      SubscriberResponseMakerFactory factory = new SubscriberResponseMakerFactory();
      SubscriberResponseMaker responseMaker = (SubscriberResponseMaker)factory.ProvideResponseMaker(Message);
      return ObtainResponseSafely(responseMaker);
    }

    private CommunicationChunk ObtainResponseSafely(SubscriberResponseMaker responseMaker) {
      CommunicationChunk result = new CommunicationChunk();
      try {
        result = responseMaker.PrepareCommunicationChunk(Message.RawMessage, Hash);
      } catch(JsonReaderException ex) {
        result.ResponseToSender = PrepareErrorResponse (ResponseCodes.JSONContractError, ex.Message);
      } catch(KeyNotFoundException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.CannotFindRequiredDataItem, ex.Message);
      } catch(UnauthorizedAccessException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.PermissionsError, ex.Message);
      } catch(DataException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.DataConstraintViolated, ex.Message);
      }
      return result;
    }

    public override string HandleInitRequest() {
      QuestionsUtils questionsUtils = new QuestionsUtils(Hash);
      QuestionsRequest simulatedRequest = generateSimulatedInitRequest();
      return questionsUtils.GetQuestionsJSON(simulatedRequest);
    }

    private QuestionsRequest generateSimulatedInitRequest() {
      QuestionsRequest simulatedRequest = new QuestionsRequest();
      simulatedRequest.RequestID = -1;
      return simulatedRequest;
    }
  }
}
