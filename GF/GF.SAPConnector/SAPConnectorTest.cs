using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.SAPConnector
{
    class SAPConnectorTest
    {
        static void Main(string[] args)
        {
            SendReplenishmentMessage();
        }
        
        public static void SendReplenishmentMessage()
        {
            string name = "CONSIGNMENT_CONNECTION";
            int poolSize = 5;
            string user = "sesi";
            string password = "sesi10";
            string language = "EN";
            string client = "000";
            string applicationServer = "rulefree10";
            string systemNumber = "53";

            string rfcFunctionName = "REPLENISHMENT_UPDATE";
            string customerName = "Shawshank Redmption";
            string customerLocation = "Upstate Maine";
            string reorderPoint = "19 years";
            string reorderAmount = "20 pick axes";
            string allowAutoReplenishment = "true";

            ConsignmentAdapter cAdapter = new ConsignmentAdapter();
            cAdapter.AddDestination(name, poolSize, user, password, language, client, applicationServer, systemNumber);
            cAdapter.TestRFCCall(name);
            cAdapter.postReplenishmentMessage(name, rfcFunctionName, customerName, customerLocation, reorderPoint, reorderAmount, allowAutoReplenishment);
            cAdapter.RemoveDestination(name);
            cAdapter = null;

            return;
        }
    }
}
