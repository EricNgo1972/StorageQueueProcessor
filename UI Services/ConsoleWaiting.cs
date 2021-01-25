using SPC;
using SPC.Services;
using SPC.Services.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexingInvoices
{
    class ConsoleWaiting : SPC.Services.UI.IWaitingPanel
    {
        void IWaitingPanel.Done()
        {
            ServicesContainer.Get<ILogService>().Log("Done","","","","");
            Console.WriteLine("Done");
        }

        void IWaitingPanel.Wait(string Title, string Message)
        {
            ServicesContainer.Get<ILogService>().Log("AzureInvoice","Index",Title,"Create", $".:| {Title}: {Message}");
            Console.WriteLine($".:| {Title}: {Message}");
        }
    }
}
