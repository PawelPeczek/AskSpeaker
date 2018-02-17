using System;
using System.Reflection;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {

	class SubscriberRequestAttribute : Attribute {

		public string RequestStrings { 
			get; 
			private set; 
		}

		internal SubscriberRequestAttribute(string requestString){
			RequestStrings = requestString;
		}

	}

	public static class RequestStrings {

		public static string GetRequestString(this SubscriberRequestTypes reqType){
			SubscriberRequestAttribute attr = GetAttr (reqType);
			return attr.RequestStrings;
		}

		private static SubscriberRequestAttribute GetAttr(SubscriberRequestTypes p){
			return (SubscriberRequestAttribute)Attribute.GetCustomAttribute(ForValue(p), typeof(SubscriberRequestAttribute));
		}

		private static MemberInfo ForValue(SubscriberRequestTypes p) {
			return typeof(SubscriberRequestTypes).GetField(Enum.GetName(typeof(SubscriberRequestTypes), p));
		}
	}

	public enum SubscriberRequestTypes {
		[SubscriberRequestAttribute("questions-request")] QuestionsRequest 
	}
}

