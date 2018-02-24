using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;

namespace AskSpeakerServer.Extensions {
	public static  class ResponseCodesExtensions {
		public static int GetResponseCodeInt(this ResponseCodes value){
			return (int)value;
		}
	}
}

