using System;
using System.Threading.Tasks;
using SPC.Helper;
using SPC.Helper.Extension;
using SPC.Services.UI;

namespace SPC.WinConsole.UIServices
{
    class ConfirmServiceConsole: SPC.Services.UI.IConfirm
    {   

        Task<bool> IConfirm.ConfirmAsync(string pFormatString, params object[] ParamArray)
        {
            Console.WriteLine(pFormatString, ParamArray);
            var ret = Console.ReadLine();
            return Task.FromResult(ret.ToBoolean());
        }
                

        void IConfirm.ShowError(Exception ex) => Console.WriteLine($"Error : {ex.Message}");
    }
}
