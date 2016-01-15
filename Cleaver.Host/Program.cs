using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceHost BankServiceHost = null;
            try
            {
                ////Base Address for MathService
                //Uri httpBaseAddress = new Uri("http://localhost:8082/BankService");

                ////Instantiate ServiceHost
                //BankServiceHost = new ServiceHost(typeof(Cleaver.Service.BankService), httpBaseAddress);

                ////Add Endpoint to Host
                ////BankServiceHost.AddServiceEndpoint(typeof(Cleaver.Service.IBankService), new WSHttpBinding(), "");
                //BankServiceHost.AddServiceEndpoint(typeof(Cleaver.Service.IBankService), new WebHttpBinding(), "");

                ////Metadata Exchange
                //ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                //serviceBehavior.HttpGetEnabled = true;
                //BankServiceHost.Description.Behaviors.Add(serviceBehavior);

                ////Open
                //BankServiceHost.Open();
                //Console.WriteLine("Service is live now at : {0}", httpBaseAddress);
                //Console.ReadLine();

                WebServiceHost host = new WebServiceHost(typeof(Cleaver.Service.BankService));
                host.Open();
                Console.WriteLine("Service running");
                Console.WriteLine("Press ENTER to stop the service");
                Console.ReadLine();
                host.Close();
            }

            catch (Exception ex)
            {
                //BankServiceHost = null;
                Console.WriteLine("There is an issue with BankService" + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
