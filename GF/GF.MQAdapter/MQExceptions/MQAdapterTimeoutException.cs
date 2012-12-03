using System;

namespace GF.MQAdapter
{
	/// <summary>
	/// Summary description for MQAdapterTimeoutException.
	/// </summary>
	public class MQAdapterTimeoutException:ApplicationException
	{
		public MQAdapterTimeoutException(string exception):base(exception)
		{
			
		}
	}
}
