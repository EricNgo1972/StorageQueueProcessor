using SPC.Services;
using SPC.CORE.Services;
using SPC.Services.UI;
using System;
using System.Collections.Generic;
using System.Text;
using SPC.Services.Cloud;
using SPC.WinConsole.UIServices;

namespace IndexingInvoices.Console_Services
{
    class RegisterServices
    {

        public static void Register()
        {
            ///UI services
            SPC.ServicesContainer.Register<IConfirm>(new ConfirmServiceConsole());
            SPC.ServicesContainer.Register<IWaitingPanel>(new ConsoleWaiting());
            SPC.ServicesContainer.Register<ILogService>(new FileLogger());
            SPC.ServicesContainer.Register<IAlert>(new AlertServiceConsole());

            SPC.Services.LogService.RegisterService(new FileLogger());

            SPC.ServicesContainer.Register<IRunURLService>(new OpenUrlService());


            /// Cloud Services
            SPC.ServicesContainer.Register<IKeyVault>(new SPC.Cloud.KeyVault_Imp());
            SPC.ServicesContainer.Register<ITable>(new SPC.Cloud.Table.TableService());
            SPC.ServicesContainer.Register<IBlobService>(new SPC.Cloud.Blob.BlobService());

            ///SPC.ServicesContainer.Register<ISendToServiceBus>(new SPC.Cloud.ServiceBus.SendToServiceBus_Imp());

            ///SPC.ServicesContainer.Register<IStorageQueue>(new SPC.Cloud.Queue.QueueService());
            ///
        }

    }
}
