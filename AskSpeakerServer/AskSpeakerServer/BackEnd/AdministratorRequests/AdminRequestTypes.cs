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
		[AdminRequestAttriute("events-info-renew")] EventsInfoRenew,
		[AdminRequestAttriute("su-check")] SuPermissionsCheck,
		[AdminRequestAttriute("event-edit")] EventEdit,
		[AdminRequestAttriute("event-close")] EventClose,
		[AdminRequestAttriute("event-create")] EventCreate,
		[AdminRequestAttriute("event-change-ownership")] EventChangeOwnership,
		[AdminRequestAttriute("event-reopen")] EventReOpen,
		[AdminRequestAttriute("question-cancell")] QuestionCancell,
		[AdminRequestAttriute("question-merge")] QuestionMerge,
		[AdminRequestAttriute("question-edit")] QuestionEdit,
		[AdminRequestAttriute("user-create")] UserCreate,
		[AdminRequestAttriute("user-delete")] UserDelete,
		[AdminRequestAttriute("password-change")] PasswordChange,
		[AdminRequestAttriute("su-password-change")] PasswordChangeWithSu
	}
}

