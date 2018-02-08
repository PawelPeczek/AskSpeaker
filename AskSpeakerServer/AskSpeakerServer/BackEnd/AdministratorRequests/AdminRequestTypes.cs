using System;
using System.Reflection;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {

	class AdminRequestAttriute : Attribute {

		public string RequestStrings { 
			get; 
			private set; 
		}

		internal AdminRequestAttriute(string requestString){
			RequestStrings = requestString;
		}
			
	}

	public static class RequestStrings {
		
		public static string GetRequestString(this AdminRequestTypes reqType){
			AdminRequestAttriute attr = GetAttr (reqType);
			return attr.RequestStrings;
		}

		private static AdminRequestAttriute GetAttr(AdminRequestTypes p){
			return (AdminRequestAttriute)Attribute.GetCustomAttribute(ForValue(p), typeof(AdminRequestAttriute));
		}
			
		private static MemberInfo ForValue(AdminRequestTypes p) {
			return typeof(AdminRequestTypes).GetField(Enum.GetName(typeof(AdminRequestTypes), p));
		}
	}

	public enum AdminRequestTypes {
		SuPermissionsCheck,
		EventEdit,
		EventClose,
		EventCreate,
		QuestionCancell,
		QuestionMerge,
		UserCreate,
		UserDelete,
		PasswordChange,
		PasswordChangeWithSu
	}
}

