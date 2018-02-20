using System;

namespace AskSpeakerServer.Extensions {
	public static class DateTimeExtensions {
		
		public static String GetTimestamp(this DateTime value) {
			return value.ToString("yyyyMMddHHmmssfff");
		}

	}
}

