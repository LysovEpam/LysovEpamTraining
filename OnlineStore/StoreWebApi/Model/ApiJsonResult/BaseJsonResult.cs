using System.Runtime.Serialization;


namespace StoreWebApi.Model.ApiJsonResult
{

	[DataContract]
	public class BaseJsonResult
	{
		[DataMember]
		public bool RequestIsProcessed { get; }
		[DataMember]
		public bool ResultIsCorrect { get; }
		[DataMember]
		public string Message { get; }
		[DataMember]
		public string JsonData { get; }

		public BaseJsonResult(bool requestIsProcessed, bool resultIsCorrect, string message, string jsonData)
		{
			RequestIsProcessed = requestIsProcessed;
			ResultIsCorrect = resultIsCorrect;
			Message = message;
			JsonData = jsonData;
		}


	}
}
