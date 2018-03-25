using System;
using System.Collections.Generic;
using System.Data;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;
using AskSpeakerServer.Extensions;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests {
  public class AdminRequestWrapper : RequestWrapper<AdminRequestTypes> {

    private IDictionary<Object, Object> Credentials;

    public AdminRequestWrapper(IDictionary<Object, Object> credentials) : this(null, credentials) { }

    public AdminRequestWrapper(PreProcessedAdminMessage message, IDictionary<Object, Object> credentials) {
      Message = message;
      Credentials = credentials;
    }

    public override CommunicationChunk HandleStandardRequest() {
      if(Message == null)
        throw new ApplicationException("Internal application error. Code 102");
      AdminResponseMakerFactory factory = new AdminResponseMakerFactory();
      AdminResponseMaker responseMaker = (AdminResponseMaker)factory.ProvideResponseMaker(Message);
      return ObtainResponseSafely(responseMaker);
    }

    private CommunicationChunk ObtainResponseSafely(AdminResponseMaker responseMaker) {
      CommunicationChunk result = new CommunicationChunk();
      try {
        result = responseMaker.PrepareCommunicationChunk(Message.RawMessage, Credentials);
      } catch(JsonReaderException ex) {
        result.ResponseToSender = PrepareErrorResponse (ResponseCodes.JSONContractError, ex.Message);
      } catch(ApplicationException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.ActivityAlreadyDone, ex.Message);
      } catch(UnauthorizedAccessException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.PermissionsError, ex.Message);
      } catch (InvalidOperationException ex){
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.InvalidOperation, ex.Message);
      } catch(ArgumentException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.WrongOriginData, ex.Message);
      } catch (KeyNotFoundException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.CannotFindRequiredDataItem, ex.Message);
      } catch(DataException ex) {
        result.ResponseToSender = PrepareErrorResponse(ResponseCodes.DataConstraintViolated, ex.Message);
      }
      return result;
    }

    public override string HandleInitRequest() {
      EventsUtils eventsUtils = new EventsUtils(Credentials);
      EventsListRequest simulatedRequest = generateSimulatedInitRequest();
      return eventsUtils.GetEventsInfoJSON(simulatedRequest);
    }

    private EventsListRequest generateSimulatedInitRequest() {
      EventsListRequest simulatedRequest = new EventsListRequest();
      simulatedRequest.RequestID = -1;
      return simulatedRequest;
    }
  }
}
