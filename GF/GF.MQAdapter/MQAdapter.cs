using System;
using System.Text;
using System.Timers;
using System.Diagnostics;
// Install Websphere client package for code from the following NS.
// Login and download client here "https: //www14.software.ibm.com/webapp/iwm/web/preLogin.do?source=swg-wasmq75"

using IBM.WMQ;

namespace GF.MQAdapter
{
	public class MQAdapter
	{
		private MQQueueManager mqQueueManager;
		private MQQueue mqPutQueue;
		private MQQueue mqGetQueue;
		
        private string mqQueueManagerName;
		private string mqRequestQueueName;

		private int characterSet;
        private int pollingTimeout;
        private const int WSMQ_DEFAULT_PORT = 1414;

		/// <summary>
		/// Instantiates the Queue Manager
		/// </summary>
		/// <param name="mqManager">The Queue Manager controlling the Request and Response Queues</param>
		public MQAdapter(string mqManager,string channel, string ipAddress,
			string putQueue,int timeout, int charSet, int port)
		{
			try
			{
				
				MQEnvironment.Hostname = ipAddress;
				MQEnvironment.Channel = channel;
                MQEnvironment.Port = WSMQ_DEFAULT_PORT;

				mqQueueManagerName = mqManager;
				mqRequestQueueName = putQueue;
				characterSet = charSet;

				pollingTimeout = timeout;

				// Connect to an MQ Manager, and share the connection handle with other threads
				mqQueueManager = new MQQueueManager(mqManager, channel, ipAddress);

				// Open Queue for Inquiry, Put Message in, and fail if Queue Manager is stopping
				mqPutQueue = mqQueueManager.AccessQueue(putQueue, MQC.MQOO_INQUIRE | 
					MQC.MQOO_OUTPUT | MQC.MQOO_FAIL_IF_QUIESCING | MQC.MQOO_SET_IDENTITY_CONTEXT);

			}	
			catch (MQException mqe)
			{
				throw new MQAdapterException("Error Code: " + 
					MQAdapterErrorReasons.GetMQFailureReasonErrorCode(mqe.Reason));
			}
		}
		
		
		/// <summary>
		/// Puts a message in an MQ Queue using the user Id provided
		/// <param name="message">The message to be put in the queue</param>
		/// <returns>Response message</returns>
		/// </summary>
		public string postMQRequestMessage(string message)
		{
			try
			{
				MQMessage requestMessage = new MQMessage();

				requestMessage.Persistence = 0;

                requestMessage.ReplyToQueueName = mqRequestQueueName;
				requestMessage.ReplyToQueueManagerName = mqQueueManagerName;

				requestMessage.Format = MQC.MQFMT_STRING;
				requestMessage.CharacterSet = characterSet;
				requestMessage.MessageType = MQC.MQMT_REQUEST;
                requestMessage.MessageId = HexaDecimalUtility.ConvertToBinary(GenerateMQMsgId());
				requestMessage.CorrelationId = requestMessage.MessageId;

				MQPutMessageOptions pmo = new MQPutMessageOptions();
				pmo.Options = MQC.MQPMO_SET_IDENTITY_CONTEXT;

				requestMessage.WriteString(message);

				mqPutQueue.Put(requestMessage, pmo);

                string _msgId = BinaryUtility.ConvertToHexaDecimal(requestMessage.MessageId);

				return _msgId;

			}
			catch (MQException mqe)
			{
				// Close request Queue if still opened
				if(mqPutQueue.OpenStatus)
					mqPutQueue.Close();
				// Close Queue manager if still opened
				if(mqQueueManager.OpenStatus)
					mqQueueManager.Close();

				throw new MQAdapterException("Error Code: " + 
					MQAdapterErrorReasons.GetMQFailureReasonErrorCode(mqe.Reason));
			}
		}

        private string GenerateMQMsgId()
		{
			string _mqMsgID = System.Convert.ToString(System.Guid.NewGuid());
			_mqMsgID = _mqMsgID.Replace("-","");
			_mqMsgID = _mqMsgID.PadRight(48, '0');

			return _mqMsgID;
		}
	}
}