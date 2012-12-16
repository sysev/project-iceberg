using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;

namespace GF.SAPConnector
{
    /// <summary>
    /// This class keeps track of all the available SAP destination configurations.  Although the 
    /// information is currently hard coded, it will eventually come from lookups in a database or 
    /// LDAP.
    /// </summary>
    public class ConsignmentDestinationConfiguration : IDestinationConfiguration
    {
        // private static ConsignmentDestinationConfiguration consignmentDestinationConfigurationInstance = null;
        Dictionary<string, RfcConfigParameters> sapDestinations;
        RfcDestinationManager.ConfigurationChangeHandler changeHandler;

        public ConsignmentDestinationConfiguration()
        {
            sapDestinations = new Dictionary<string, RfcConfigParameters>();
            initConfig(this);
        }

        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters foundDestination;
            sapDestinations.TryGetValue(destinationName, out foundDestination);
            return foundDestination;
        }

        // our configuration supports events
        public bool ChangeEventsSupported()
        {
            return true;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged
        {
            add
            {
                changeHandler = value;
            }
            remove
            {
                //do nothing
            }
        }

        //allows adding or modifying a destination for a specific application server
        public void AddEditDestination(string name, int poolSize, string user, string password, string language,
            string client, string applicationServer, string systemNumber)
        {
            // We need to check the given parameters for validity, e.g. that name is not null as this is not 
            // relevant
            RfcConfigParameters newConfigParameters = new RfcConfigParameters();
            newConfigParameters[RfcConfigParameters.Name] = name;
            newConfigParameters[RfcConfigParameters.PeakConnectionsLimit] = Convert.ToString(poolSize);
            newConfigParameters[RfcConfigParameters.IdleTimeout] = Convert.ToString(10); // we keep connections for 10 minutes
            newConfigParameters[RfcConfigParameters.User] = user;
            newConfigParameters[RfcConfigParameters.Password] = password;
            newConfigParameters[RfcConfigParameters.Client] = client;
            newConfigParameters[RfcConfigParameters.Language] = language;
            newConfigParameters[RfcConfigParameters.AppServerHost] = applicationServer;
            newConfigParameters[RfcConfigParameters.SystemNumber] = systemNumber;
            RfcConfigParameters existingConfiguration;
            //if a destination of that name existed before, we need to fire a change event
            if (sapDestinations.TryGetValue(name, out existingConfiguration))
            {
                sapDestinations[name] = newConfigParameters;
                RfcConfigurationEventArgs eventArgs = new RfcConfigurationEventArgs(RfcConfigParameters.EventType.CHANGED, newConfigParameters);
                Console.WriteLine("Fire change event " + eventArgs.ToString() + " for destination " + name);
                changeHandler(name, eventArgs);
            }
            else
            {
                sapDestinations[name] = newConfigParameters;
            }
            Console.WriteLine("Added application server destination " + name);
            
            // print the destination attributwes
            printDestination(name);
        }

        //removes the destination that is known under the given name
        public void RemoveDestination(string name)
        {
            if (name != null && sapDestinations.Remove(name))
            {
                Console.WriteLine("Successfully removed destination " + name);
                Console.WriteLine("Fire deletion event for destination " + name);
                changeHandler(name, new RfcConfigurationEventArgs(RfcConfigParameters.EventType.DELETED));
            }
        }

        //allows adjusting the pool size of existing destinations at runtime
        //if such a destination existed
        public bool AdjustPoolSize(string destinationName, int newPoolSize)
        {
            if (destinationName != null)
            {
                RfcConfigParameters existingConfiguration;
                //if a destination of that name exists, we can actually adjust it
                if (sapDestinations.TryGetValue(destinationName, out existingConfiguration))
                {
                    existingConfiguration[RfcConfigParameters.PeakConnectionsLimit] = Convert.ToString(newPoolSize);
                    RfcConfigurationEventArgs eventArgs = new RfcConfigurationEventArgs(RfcConfigParameters.EventType.CHANGED, existingConfiguration);
                    Console.WriteLine("Fire change event " + eventArgs.ToString() + " (poolsize adjusted) for destination " + destinationName);
                    changeHandler(destinationName, eventArgs);
                    return true;
                }
            }
            return false;
        }

        //allows adding or modifying a destination for a specific application server
        public void printDestination(string name)
        {
            RfcDestination destination1 = RfcDestinationManager.GetDestination(name);
            destination1.Ping();
            Console.WriteLine(AttributesToString(destination1.SystemAttributes));
            Console.WriteLine();
        }

        //removes the destination that is known under the given name
        public void ClearAllDestinations()
        {
            foreach (var sapDestination in sapDestinations)
            {
                RemoveDestination(sapDestination.Key);
            }
        }

        public static void initConfig(ConsignmentDestinationConfiguration consignmentDestinationConfiguration)
        {
            RfcDestinationManager.RegisterDestinationConfiguration(consignmentDestinationConfiguration);
            Console.WriteLine("Registered new configuration");
        }

        public static void clearConfig(ConsignmentDestinationConfiguration consignmentDestinationConfiguration)
        {
            consignmentDestinationConfiguration.ClearAllDestinations();
            RfcDestinationManager.UnregisterDestinationConfiguration(consignmentDestinationConfiguration);
            Console.WriteLine("Unregistered configuration successfully");
        }



        private static String AttributesToString(RfcSystemAttributes attr)
        {
            string result = "Attributes:\n";
            result += "Destination " + attr.Destination + "\n";
            result += "HostName " + attr.HostName + "\n";
            result += "User " + attr.User + "\n";
            result += "Client " + attr.Client + "\n";
            result += "ISOLanguage " + attr.ISOLanguage + "\n";
            result += "Language " + attr.Language + "\n";
            result += "KernelRelease " + attr.KernelRelease + "\n";
            result += "CodePage " + attr.CodePage + "\n";
            result += "PartnerCodePage " + attr.PartnerCodePage + "\n";
            result += "PartnerHost " + attr.PartnerHost + "\n";
            result += "PartnerRelease " + attr.PartnerRelease + "\n";
            result += "PartnerReleaseNumber " + attr.PartnerReleaseNumber + "\n";
            result += "PartnerType " + attr.PartnerType + "\n";
            result += "Release " + attr.Release + "\n";
            result += "RfcRole " + attr.RfcRole + "\n";
            result += "SystemID " + attr.SystemID + "\n";
            result += "SystemNumber " + attr.SystemNumber + "\n";
            return result;
        }

    }

}