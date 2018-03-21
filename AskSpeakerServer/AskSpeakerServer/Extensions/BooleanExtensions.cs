using System;

namespace AskSpeakerServer.Extensions {
	
	public static class BooleanExtensions {
		public static short ConvertVote(this Boolean value) {
			short result = 1;
			if (!value)
				result = -1;
			return result;
				
		}
	}

}

