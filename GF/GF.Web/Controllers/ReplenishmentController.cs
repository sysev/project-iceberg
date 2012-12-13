using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GF.Web.Controllers
{
    public class ReplenishmentController : Controller
    {
        //
        // GET: /Replenishment/

        public ActionResult Index()
        {
            return View();
        }

        public String SendMQMessage(String autoReplenishMessage)
        {
            String messageText = autoReplenishMessage;
            if (autoReplenishMessage == null || autoReplenishMessage.Trim().Length == 0)
            {
                messageText = "This is a Auto Replenish Message!";
            }
            //  We need to setup MQ to test this feature.
            // I set up MQ server on my local host with the following features:
            // queueManagerName = "QM_APPLE"
            // channelName = "SAPConnection";
            // ipAddress = "rulefree10"; // localhost
            // queue name = Q1
            // port = 1414 (default)
            // queue manager security = disabled

            String queueManagerName = "QM_APPLE";
            String channelName = "MyConnection";
            String ipAddress = "rulefree10"; // can be any http or https address
            String putQueue = "Q1";
            int timeout = 1500;
            int charset = 500;
            int portNum = 1414; // default port

            /*MQAdapter.MQAdapter mqAdapter = new MQAdapter.MQAdapter(queueManagerName, channelName, ipAddress,
                putQueue, timeout, charset, portNum);
            String responseString = mqAdapter.postMQRequestMessage(messageText);
            return responseString;*/
            return "";
        }

    }
}
