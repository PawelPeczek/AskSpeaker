using System;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;

namespace AskSpeakerServer.BackEnd.RequestHandlers {
  public class AdminResponseMakerFactory : ResponseMakerFactory<AdminRequestTypes> {
    
    public ResponseMaker<AdminRequestTypes> ProvideResponseMaker(PreProcessedMessage<AdminRequestTypes> request) {
      AdminResponseMaker result = null;
      switch(request.RequestType) {
        case AdminRequestTypes.EventsInfoRenew:
          result = new EventsInfoResponseMaker();
          break;
        case AdminRequestTypes.SuPermissionsCheck:
          result = new SuPermissionsCheckResponseMaker();
          break;
        case AdminRequestTypes.EventClose:
          result = new EventCloseResponseMaker();
          break;
        case AdminRequestTypes.EventReOpen:
          result = new EventReOpenResponseMaker();
          break;
        case AdminRequestTypes.EventEdit:
          result = new EventEditResponseMaker();
          break;
        case AdminRequestTypes.EventCreate:
          result = new EventCreateResponseMaker();
          break;
        case AdminRequestTypes.EventChangeOwnership:
          result = new EventChangeOwnershipResponseMaker();
          break;
        case AdminRequestTypes.QuestionCancell:
          result = new QuestionCancellResponseMaker();
          break;
        case AdminRequestTypes.QuestionMerge:
          result = new QuestionMergeResponseMaker();
          break;
        case AdminRequestTypes.QuestionEdit:
          result = new QuestionEditResponseMaker();
          break;
        case AdminRequestTypes.UserCreate:
          result = new UserCreateResponseMaker();
          break;
        case AdminRequestTypes.UserDelete:
          result = new UserDeleteResponseMaker();
          break;
        case AdminRequestTypes.PasswordChange:
          result = new PasswordChangeResponseMaker();
          break;
        case AdminRequestTypes.PasswordChangeWithSu:
          result = new PasswordChangeWithSuResponseMaker();
          break;
      }
      return result;
    }
  }
}
