using Microsoft.WindowsAzure.Storage.Queue;
using SPC.Helper;
using SPC.Helper.Extension;
using SPC.Services.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPC.Cloud.Queue
{
    [Serializable]
    public class StorageQueueProcessor
    {
        private string _storageAccount;
        private string _queueName;
              
        private List<Func<CloudQueueMessage, Task>> _asyncActions;

        public StorageQueueProcessor(string pStorageAccount, string pQueueName, List<Func<CloudQueueMessage, Task>> pAsyncActions )
        {
            _storageAccount = pStorageAccount;
            _queueName = pQueueName;
            _asyncActions = pAsyncActions;
 

        }

        public async Task<int> ProcessMessagesAsync()
        {

            if (_asyncActions != null & _asyncActions.Count > 0)
            {

                var theQueue = await (new QueueClient(_queueName, _storageAccount)).GetQueueAsync();

                await theQueue.FetchAttributesAsync();

                var msgCount = theQueue.ApproximateMessageCount;

                if (msgCount > 0)
                {
                    var idx = 0;

                    while (idx <= msgCount - 1)
                    {
                        var msg = await theQueue.GetMessageAsync();

                        if (msg != null )
                        {
                                                      
                            ServicesContainer.Get<IWaitingPanel>().Wait(ResStrConst.ProcessItem("Message"), msg.Id);

                            if (await ProcessBlobMsgAsync(msg))
                            {
                                await theQueue.DeleteMessageAsync(msg);

                                idx += 1;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }

                    SPC.ServicesContainer.Get<IAlert>().Alert($"Processed {msgCount} messages.");

                    return idx;
                }
                else
                {
                    SPC.ServicesContainer.Get<IAlert>().Alert("No message to process!!!".Translate());
                }

            }
            else
            {
                SPC.ServicesContainer.Get<IAlert>().Alert("No processing actions defined".Translate());
            }

            // End If

            return 0;
        }

        private async Task<bool> ProcessBlobMsgAsync(CloudQueueMessage pMsg)
        {
            //var tasks = new List<Task>();
            if (_asyncActions != null & _asyncActions.Count > 0)
            {
                foreach (var act in _asyncActions)
                {
                    var theTask = act(pMsg);
                    await theTask;
                }

            }

            return true;

        }

     
    }
}
