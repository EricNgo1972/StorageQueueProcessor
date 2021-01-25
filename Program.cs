
using IndexingInvoices.Console_Services;
using System;
using System.Threading.Tasks;

namespace IndexingInvoices
{
    class Program
    {
        public static async Task Main()
        {
            SayHello();

            RegisterServices.Register();

            var worker = new TicketQueueWorker();

            await worker.RunAsync();


            Console.WriteLine("Enter any Key to exit");
            Console.ReadKey();

        }




      

        private static void SayHello()
        {
            Console.WriteLine("Welcome to Phoebus Console !!!");
            Console.WriteLine("--------------------------------");

        }              

 

 
    }
}
