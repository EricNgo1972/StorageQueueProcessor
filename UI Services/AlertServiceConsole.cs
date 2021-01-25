using SPC.Services.UI;
using System;
 


namespace SPC.WinConsole.UIServices
{
    class AlertServiceConsole: Services.UI.IAlert
    {
       
        void IAlert.Alert(string pFormatString, params object[] ParamArray) => Console.WriteLine(pFormatString, ParamArray);
      
        void IAlert.ShowError(Exception ex) => Console.WriteLine($"Error : {ex.Message}");
        void IAlert.Toast(string pFormatString, params object[] ParamArray) => Console.WriteLine(pFormatString, ParamArray);
    }
}
