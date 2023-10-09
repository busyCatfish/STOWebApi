using System.Runtime.Serialization;

namespace STOWebApi.Business.Validation
{
	public class STOSystemException : Exception
	{
		public STOSystemException()
		{
		}

		public STOSystemException(string? message) : base(message)
		{
		}

		public STOSystemException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
