
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPC.Helper.Extension;
using SPC.Cloud.Queue;
using Microsoft.WindowsAzure.Storage.Queue;
using System.ComponentModel.Design;
using SPC.Services.UI;

namespace IndexingInvoices
{
    class TicketQueueWorker
    {

        string _account = "spc";
        string _queueName = "support-tickets";

        public async Task RunAsync()
        {

            List<Func<CloudQueueMessage, Task>> _actions = new List<Func<CloudQueueMessage, Task>>();
            _actions.Add(Process_01);
            _actions.Add(Process_02);

            var processor = new StorageQueueProcessor(_account, _queueName, _actions);

            await processor.ProcessMessagesAsync();
        }


        #region Processing Algorithms


        private static Task Process_02(CloudQueueMessage pMsg)
        {
            SPC.ServicesContainer.Get<IWaitingPanel>().Wait("Algorithm o2 ", pMsg.Id);

            //Process your message here

            return Task.CompletedTask;
        }

        private static Task Process_01(CloudQueueMessage pMsg)
        {
            SPC.ServicesContainer.Get<IWaitingPanel>().Wait("Algorithm o1 ", pMsg.Id);

            //Process your message also here
            // chain up your processing unit ....

            return Task.CompletedTask;
        }


        #endregion

    }
}
