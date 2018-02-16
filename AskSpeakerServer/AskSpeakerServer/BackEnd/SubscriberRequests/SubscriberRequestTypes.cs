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

		public static string GetRequestString(this AdminRequestTypes reqType){
			SubscriberRequestAttribute attr = GetAttr (reqType);
			return attr.RequestStrings;
		}

		private static SubscriberRequestAttribute GetAttr(AdminRequestTypes p){
			return (SubscriberRequestAttribute)Attribute.GetCustomAttribute(ForValue(p), typeof(SubscriberRequestAttribute));
		}

		private static MemberInfo ForValue(AdminRequestTypes p) {
			return typeof(AdminRequestTypes).GetField(Enum.GetName(typeof(AdminRequestTypes), p));
		}
	}

	public enum AdminRequestTypes {
		[SubscriberRequestAttribute("initial-request")] InitialRequest 
	}
}

