using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.Extensions {
	public static  class ResponseCodesExtensions {
		public static int GetResponseCodeInt(this ResponseCodes value){
			return (int)value;
		}
	}
}

