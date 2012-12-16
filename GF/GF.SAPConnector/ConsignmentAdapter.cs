using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;

namespace GF.SAPConnector
{

    public class ConsignmentAdapter
    {
		/* See the App.config file for the definition of the following RFC destinations.
		 * Modify the logon parameters in that file to match a SAP system in your local network.
		 * The available configuration parameters are described in the documentation of the
		 * class RfcConfiguration.
		 */
        ConsignmentDestinationConfiguration configDestinations = null;

        public ConsignmentAdapter()
        {
            configDestinations = new ConsignmentDestinationConfiguration();
        }

        public ConsignmentAdapter(string name, int poolSize, string user, string password, string language,
            string client, string applicationServer, string systemNumber)
        {
            configDestinations = new ConsignmentDestinationConfiguration();
            AddDestination(name, poolSize, user, password, language, client, applicationServer, systemNumber);
        }

        public void AddDestination(string name, int poolSize, string user, string password, string language,
            string client, string applicationServer, string systemNumber)
        {
            configDestinations.AddEditDestination(name, poolSize, user, password, language, client, applicationServer, systemNumber);
            configDestinations.printDestination(name);
        }

        public void RemoveDestination(string name)
        {
            configDestinations.RemoveDestination(name);
        }

        // Copied this function from somewhere.  It makes a RFC call to a metadata function.  
        // Not completely sure what/how it does that
        public void TestRFCCall(string destinationName)
        {
            RfcDestination destination = RfcDestinationManager.GetDestination(destinationName);
            IRfcFunction function = null;
            try
            {
                function = destination.Repository.          //get metadata repository associated with the destination
                                    CreateFunction("STFC_CONNECTION");  //fetch or get cached function metadata and 
                //create a function container based on the function metadata

                //set the parameter REQUTEXT. The parameter is CHAR 255, but the .Net Connector runtime always trys to find
                //a suitable conversion between C# data types and ABAP data types.
                function.SetValue("REQUTEXT", "Hello SAP");
                function.Invoke(destination); //make the remote call
            }
            catch (RfcBaseException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }

            Console.WriteLine("STFC_CONNECTION finished:");
            //Read the function parameters ECHOTEXT and RESPTEXT
            Console.WriteLine(" Echo: " + function.GetString("ECHOTEXT"));
            Console.WriteLine(" Response: " + function.GetString("RESPTEXT"));
            Console.WriteLine();
        }



        public void postReplenishmentMessage (string destinationName, string rfcName, string customerName, string customerLocation, 
            string reorderPoint, string reorderAmount, string allowAutoReplenishment)
        {
            RfcDestination destination = RfcDestinationManager.GetDestination(destinationName);
            IRfcFunction function = null;
            try
            {
                function = destination.Repository.CreateFunction(rfcName);
                //function.SetValue("REQUTEXT", "Hello SAP");
                function.SetValue("CUSTOMER_NAME", customerName);
                function.SetValue("CUSTOMER_LOCATION", customerLocation);
                function.SetValue("REORDER_POINT", reorderPoint);
                function.SetValue("REORDER_AMOUNT", reorderAmount);
                function.SetValue("ALLOW_AUTO_REPLENISHMENT", allowAutoReplenishment);

                // make the remote call
                function.Invoke(destination); 
            }
            catch (RfcBaseException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }

            Console.WriteLine(rfcName + " finished:");
            //Read the function parameters ECHOTEXT and RESPTEXT
            Console.WriteLine(" Echo: " + function.GetString("ECHOTEXT"));
            Console.WriteLine(" Response: " + function.GetString("RESPTEXT"));
            Console.WriteLine();
        }
        
        
        
        
        // destructor
        ~ConsignmentAdapter()  
        {
            ConsignmentDestinationConfiguration.clearConfig(configDestinations);
        }



    }
}
